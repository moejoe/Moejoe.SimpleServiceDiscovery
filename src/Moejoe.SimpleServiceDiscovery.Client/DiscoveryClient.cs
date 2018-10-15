using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Moejoe.SimpleServiceDiscovery.Client.Internal;
using MoeJoe.SimpleServiceDiscovery.Models;

namespace Moejoe.SimpleServiceDiscovery.Client
{


    public class DiscoveryClient : IDiscoveryClient
    {
        private readonly HttpClient _channel;

        public DiscoveryClient(HttpClient channel)
        {
            _channel = channel ?? throw new ArgumentNullException(nameof(channel));
        }


        public async Task<DiscoveryResponse> DiscoverAsync(string serviceDefinition, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(serviceDefinition))
            {
                throw new ArgumentException("Argument Must not be null or whitespace.", nameof(serviceDefinition));
            }

            using (var response = await _channel.GetAsync(DiscoveryApi.Discover.DiscoverService(serviceDefinition), cancellationToken))
            {
                var responseStream = await response.Content.ReadAsStreamAsync();

                if (response.IsSuccessStatusCode)
                {
                    return responseStream.DeserializeFromStream<ServiceInstance[]>()?
                        .Select(p => new DiscoveryResponse
                        {
                            BaseUrl = new Uri(p.BaseUrl, UriKind.Absolute),
                            IsError = false
                        }).FirstOrDefault() ?? throw new UnexpectedResponseContentException();
                }
                var error = responseStream.DeserializeFromStream<Error>();
                if (error.Type == DiscoveryApi.ErrorTypes.ServiceNotFound)
                {
                    return DiscoveryResponse.DoesNotExist();
                }
                throw new DiscoveryException(error);
            }
        }

        public DiscoveryResponse Discover(string serviceDefinition)
        {
            return DiscoverAsync(serviceDefinition, CancellationToken.None).Result;
            
        }
    }
    public class UnexpectedResponseContentException : Exception
    {

    }
    public class DiscoveryException : Exception
    {
        public DiscoveryException(Error error) : base($"{error.Title}: ${error.Detail}") { }
        public DiscoveryException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}