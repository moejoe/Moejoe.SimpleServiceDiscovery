using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;
using Moejoe.SimpleServiceDiscovery.Server.Storage.Models;
using Moejoe.SimpleServiceDiscovery.Server.Storage.Stores;

namespace Moejoe.SimpleServiceDiscovery.Server.Stores.InMemory
{
    public class InMemoryServiceRegistryStore : IServiceRegistryStore
    {
        private static readonly ConcurrentDictionary<string, ServiceInstanceDao> Instances = new ConcurrentDictionary<string, ServiceInstanceDao>();

        public Task<ServiceInstanceDao[]> FindByServiceDefinitionAsync(string serviceDefinition)
        {
            return Task.FromResult(Instances.Where(p => p.Value?.ServiceDefinition == serviceDefinition).Select(p => p.Value).ToArray());
        }

        public Task<ServiceInstanceDao> GetAsync(string id)
        {
            if (Instances.TryGetValue(id, out ServiceInstanceDao existing))
            {
                return Task.FromResult(existing);
            }
            return Task.FromResult<ServiceInstanceDao>(null);
        }

        public Task RemoveAsync(string id)
        {
            Instances.TryRemove(id, out ServiceInstanceDao removed);
            return Task.CompletedTask;
        }

        public Task StoreAsync(ServiceInstanceDao serviceInstance)
        {
            Instances[serviceInstance.Id] = serviceInstance;
            return Task.CompletedTask;
        }
    }
}
