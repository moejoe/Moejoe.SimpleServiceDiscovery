using System;

namespace Moejoe.SimpleServiceDiscovery.Client
{
    public class DiscoveryResponse
    {
        public bool IsError { get; set; }
        public Uri BaseUrl { get; set; }

        public DiscoveryResponse()
        {

        }

        public DiscoveryResponse(Uri baseUrl)
        {
            BaseUrl = baseUrl;
            IsError = false;
        }
        internal static DiscoveryResponse DoesNotExist()
        {
            return new DiscoveryResponse
            {
                IsError = true
            };
        }
    }
}