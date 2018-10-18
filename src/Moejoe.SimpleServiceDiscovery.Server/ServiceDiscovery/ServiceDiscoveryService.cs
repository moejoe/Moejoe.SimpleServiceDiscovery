using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moejoe.SimpleServiceDiscovery.Common.Models;
using Moejoe.SimpleServiceDiscovery.Server.Logging;
using Moejoe.SimpleServiceDiscovery.Server.Storage.Stores;

namespace Moejoe.SimpleServiceDiscovery.Server.ServiceDiscovery
{
    public class ServiceDiscoveryService : IServiceDiscoveryService
    {
        private readonly IServiceRegistryStore _serviceRegistryStore;
        private readonly ILogger _logger;

        public ServiceDiscoveryService(IServiceRegistryStore serviceRegistryStore, ILogger<ServiceDiscoveryService> logger)
        {
            _logger = logger;
            _serviceRegistryStore = serviceRegistryStore ?? throw new ArgumentNullException(nameof(serviceRegistryStore));
        }

        public async Task<ServiceDiscoveryResult> DiscoverAsync(string serviceDefinition)
        {
            _logger.LogDebug("Service Discovery started for service {serviceDefinition}", serviceDefinition);
            var instances = (await _serviceRegistryStore.FindByServiceDefinitionAsync(serviceDefinition)).Select(p => new ServiceInstance
            {
                ServiceDefinition = p.ServiceDefinition,
                Id = p.Id,
                BaseUrl = p.BaseUrl

            }).ToArray();
            var result = new ServiceDiscoveryResult
            {
                Instances = instances
            };
            var logObject = new ServiceDiscoveryResultLog(result);
            _logger.LogInformation($"Service Discovery discovered {instances.Length} instances.");
            _logger.LogDebug("Service Discovery Result: \n{logObject}", logObject);
            return result;
        }
    }
}
