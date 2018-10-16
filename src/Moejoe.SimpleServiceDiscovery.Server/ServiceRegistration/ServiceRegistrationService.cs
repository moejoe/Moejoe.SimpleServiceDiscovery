using System;
using System.Linq;
using System.Threading.Tasks;
using Moejoe.SimpleServiceDiscovery.Common.Models;
using Moejoe.SimpleServiceDiscovery.Server.Infrastructure;
using Moejoe.SimpleServiceDiscovery.Storage.Stores;

namespace Moejoe.SimpleServiceDiscovery.Server.ServiceRegistration
{

    public class ServiceRegistrationService : IServiceRegistrationService
    {
        private readonly IServiceRegistryStore _serviceRegistryStore;

        public ServiceRegistrationService(IServiceRegistryStore serviceRegistryStore)
        {
            _serviceRegistryStore = serviceRegistryStore ?? throw new ArgumentNullException(nameof(serviceRegistryStore));
        }

        public async Task<ServiceRegistrationResult> RegisterAsync(ServiceInstance service)
        {
            var instance = await _serviceRegistryStore.GetAsync(service.Id);
            
            if (instance != null)
            {
                throw new ServiceInstanceAlreadyExistsException(service.Id);
            }
            await _serviceRegistryStore.StoreAsync(service.ToDao());
            return ServiceRegistrationResult.Success;
        }

        public async Task UnregisterAsync(string id)
        {
            var instance = await _serviceRegistryStore.GetAsync(id);
            if (instance == null)
            {
                throw new ServiceInstanceNotFoundException(id);
            }
            await _serviceRegistryStore.RemoveAsync(id);
        }
    }
}
