using MoeJoe.SimpleServiceDiscovery.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Moejoe.SimpleServiceDiscovery.Client.Internal
{
    internal static class StreamExtensions
    {
        internal static TContent DeserializeFromStream<TContent>(this Stream stream)
        {
            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream));
            }
            using (var streamReader = new StreamReader(stream))
            using (var jsonReader = new JsonTextReader(streamReader))
            {
                var serializer = new JsonSerializer();
                var result = serializer.Deserialize<TContent>(jsonReader);
                return result;
            }
        }

        internal static async Task<HttpResponseMessage> PostJsonAsync<TContent>(this HttpClient client, Uri requestUri, TContent model, CancellationToken cancellationToken)
        {
            using (StringWriter sw = new StringWriter())
            using (JsonTextWriter writer = new JsonTextWriter(sw))
            {
                var serializer = new JsonSerializer();
                serializer.Serialize(writer, model);
                var content = new StringContent(sw.ToString(), Encoding.UTF8, "application/json")
                {
                    
                };
                return await client.PostAsync(requestUri, content, cancellationToken);
            }



        }

    }
}
