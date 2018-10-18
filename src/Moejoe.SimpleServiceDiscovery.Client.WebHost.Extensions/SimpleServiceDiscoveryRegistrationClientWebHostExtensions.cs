using System;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Moejoe.SimpleServiceDiscovery.Client;
using Moejoe.SimpleServiceDiscovery.Common.Models;

namespace Microsoft.AspNetCore.Hosting
{
    public static class SimpleServiceDiscoveryRegistrationClientWebHostExtensions
    {
        public static async Task RegisterAndRunAsync(this IWebHost webHost, string discoveryServiceUrl, string serviceDefinition, string serviceId = null, string baseUrl = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            var registrationClient = new RegistrationClient(new HttpClient
            {
                BaseAddress = new Uri(discoveryServiceUrl)
            });
            if (baseUrl == null)
            {
                var addresses = webHost.ServerFeatures.Get<IServerAddressesFeature>();
                baseUrl = addresses.Addresses.First();
                baseUrl = baseUrl.Replace("localhost", Environment.MachineName);
            }

            var instance = new ServiceInstance
            {
                BaseUrl = baseUrl,
                Id = serviceId ?? Guid.NewGuid().ToString(),
                ServiceDefinition = serviceDefinition
            };
            await registrationClient.RegisterAsync(instance, cancellationToken);
            try
            {
                await webHost.RunAsync(cancellationToken);
            }
            finally
            {
                try
                {
                    await registrationClient.UnregisterAsync(instance.Id, cancellationToken);

                }
                catch
                {
                    //don't throw
                }

            }
        }
    }
}
