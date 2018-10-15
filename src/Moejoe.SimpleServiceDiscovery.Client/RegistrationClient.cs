using System;
using System.Net.Http;

namespace Moejoe.SimpleServiceDiscovery.Client
{
    public class RegistrationClient : IRegistrationClient
    {
        private RegistrationClientOptions _options;
        private HttpClient _channel;


        public RegistrationClient(RegistrationClientOptions options)
        {
            _options = options;
            _channel = new HttpClient();
        }

        public RegistrationResponse Register(RestServiceInstance definition)
        {
            throw new NotImplementedException();
        }
    }

    public class RegistrationClientOptions
    {
        public string AgentEndpoint { get; set; }
    }


}
