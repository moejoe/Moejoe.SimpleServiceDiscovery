using System;
using System.Linq;
using System.Threading.Tasks;
using Moejoe.SimpleServiceDiscovery.Common.Models;
using Moejoe.SimpleServiceDiscovery.Server.Storage.Stores;

namespace Moejoe.SimpleServiceDiscovery.Server.ServiceDiscovery
{
    public class ServiceDiscoveryService : IServiceDiscoveryService
    {
        private readonly IServiceRegistryStore _serviceRegistryStore;

        public ServiceDiscoveryService(IServiceRegistryStore serviceRegistryStore)
        {
            _serviceRegistryStore = serviceRegistryStore ?? throw new ArgumentNullException(nameof(serviceRegistryStore));
        }

        public async Task<ServiceDiscoveryResult> DiscoverAsync(string serviceDefinition)
        {
            var instances = (await _serviceRegistryStore.FindByServiceDefinitionAsync(serviceDefinition)).Select(p => new ServiceInstance
            {
                ServiceDefinition = p.ServiceDefinition,
                Id = p.Id,
                BaseUrl = p.BaseUrl

            }).ToArray();
            return new ServiceDiscoveryResult
            {
                Instances = instances
            };
        }
    }
}
