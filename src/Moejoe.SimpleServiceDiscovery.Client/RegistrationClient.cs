using Moejoe.SimpleServiceDiscovery.Common;
using Moejoe.SimpleServiceDiscovery.Common.Models;
using Moejoe.SimpleServiceDiscovery.Client.Internal;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Moejoe.SimpleServiceDiscovery.Client
{
    public class RegistrationClient : IRegistrationClient
    {
        private HttpClient _channel;

        public RegistrationClient(HttpClient channel)
        {
            _channel = channel ?? throw new ArgumentNullException(nameof(channel));
        }

        public async Task<RegistrationResponse> RegisterAsync(ServiceInstance definition, CancellationToken cancellationToken)
        {
            using (var response = await _channel.PostJsonAsync(DiscoveryApi.Register.RegisterService, definition, cancellationToken))
            {
                var content = await response.Content.ReadAsStreamAsync();
                if (response.IsSuccessStatusCode)
                {
                    return RegistrationResponse.SuccessResponse;
                }
                else
                {
                    var error = content.DeserializeFromStream<Error>();
                    throw new DiscoveryException(error);
                }
            }
        }
    }

}
