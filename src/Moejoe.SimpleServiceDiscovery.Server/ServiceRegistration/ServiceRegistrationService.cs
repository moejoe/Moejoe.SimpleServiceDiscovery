using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moejoe.SimpleServiceDiscovery.Common.Models;
using Moejoe.SimpleServiceDiscovery.Server.Infrastructure;
using Moejoe.SimpleServiceDiscovery.Server.Logging;
using Moejoe.SimpleServiceDiscovery.Server.Storage.Stores;

namespace Moejoe.SimpleServiceDiscovery.Server.ServiceRegistration
{

    public class ServiceRegistrationService : IServiceRegistrationService
    {
        private readonly IServiceRegistryStore _serviceRegistryStore;
        private readonly ILogger _logger;

        public ServiceRegistrationService(IServiceRegistryStore serviceRegistryStore, ILogger<ServiceRegistrationService> logger)
        {
            _serviceRegistryStore = serviceRegistryStore ?? throw new ArgumentNullException(nameof(serviceRegistryStore));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<ServiceRegistrationResult> RegisterAsync(ServiceInstance service)
        {
            var logObject = new ServiceInstanceLog(service);
            _logger.LogDebug("Registering Service: {logObject}", logObject);

            var instance = await _serviceRegistryStore.GetAsync(service.Id);
            if (instance != null)
            {
                _logger.LogError("ServiceInstance '{service.Id}' already exists.", service.Id);
                throw new ServiceInstanceAlreadyExistsException(service.Id);
            }
            await _serviceRegistryStore.StoreAsync(service.ToDao());
            _logger.LogInformation($"Registered Instance: {service.Id} for Service: {service.ServiceDefinition}");
            return ServiceRegistrationResult.Success;
        }

        public async Task UnregisterAsync(string id)
        {
            _logger.LogDebug("Unregistering Service: {id}", id);
            var instance = await _serviceRegistryStore.GetAsync(id);
            if (instance == null)
            {
                _logger.LogError("Service Instance does not exist {id}", id);
                throw new ServiceInstanceNotFoundException(id);
            }
            await _serviceRegistryStore.RemoveAsync(id);
            _logger.LogInformation($"Unregistered Instance: {id} for Service: {instance.ServiceDefinition}");
        }


    }
}
