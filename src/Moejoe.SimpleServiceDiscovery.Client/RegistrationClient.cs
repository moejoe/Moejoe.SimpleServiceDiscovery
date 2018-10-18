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
        private readonly HttpClient _channel;

        public RegistrationClient(HttpClient channel)
        {
            _channel = channel ?? throw new ArgumentNullException(nameof(channel));
        }

        public async Task<RegistrationResponse> RegisterAsync(ServiceInstance definition, CancellationToken cancellationToken = default(CancellationToken))
        {
            using (var response = await _channel.PostJsonAsync(DiscoveryApi.Register.RegisterService, definition, cancellationToken))
            {
                var content = await response.Content.ReadAsStreamAsync();
                if (response.IsSuccessStatusCode)
                {
                    return RegistrationResponse.SuccessResponse;
                }
                var error = content.DeserializeFromStream<Error>() ?? throw DiscoveryException.UnexpectedStatusCodeWithNoErrorContent(response);
                throw new DiscoveryException(error);
            }
        }

        public async Task UnregisterAsync(string id, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentException("must not be null or whitespace", nameof(id));
            }

            using (var response = await _channel.DeleteAsync(DiscoveryApi.Register.UnregisterService(id),  cancellationToken))
            {
                var content = await response.Content.ReadAsStreamAsync();
                if (response.IsSuccessStatusCode)
                {
                    return;
                }
                var error = content.DeserializeFromStream<Error>() ?? throw DiscoveryException.UnexpectedStatusCodeWithNoErrorContent(response);
                throw new DiscoveryException(error);
            }
        }
    }

}
