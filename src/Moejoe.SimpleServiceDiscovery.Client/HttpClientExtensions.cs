using System.Net.Http;

namespace Moejoe.SimpleServiceDiscovery.Client
{
    public static class HttpClientExtensions
    {
        public static HttpClient ApplyTo(this DiscoveryResponse response, HttpClient httpClient)
        {
            httpClient.BaseAddress = response.BaseUrl;
            return httpClient;
        }
    }
}
