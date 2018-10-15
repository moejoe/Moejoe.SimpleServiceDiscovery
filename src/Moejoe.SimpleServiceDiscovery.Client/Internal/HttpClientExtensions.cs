using MoeJoe.SimpleServiceDiscovery.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Moejoe.SimpleServiceDiscovery.Client.Internal
{
    internal static class StreamExtensions
    {
        internal static TModel DeserializeFromStream<TModel>(this Stream stream)
        {
            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream));
            }
            using (var streamReader = new StreamReader(stream))
            using (var jsonReader = new JsonTextReader(streamReader))
            {
                var serializer = new JsonSerializer{ 
                    
                };
                var result = serializer.Deserialize<TModel>(jsonReader);
                return result;
            }
        }

    }
}
