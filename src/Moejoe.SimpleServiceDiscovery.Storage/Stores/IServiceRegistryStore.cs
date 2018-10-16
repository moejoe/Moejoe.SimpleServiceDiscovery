using System.Threading.Tasks;
using Moejoe.SimpleServiceDiscovery.Storage.Models;

namespace Moejoe.SimpleServiceDiscovery.Storage.Stores
{
    public interface IServiceRegistryStore
    {
        Task StoreAsync(ServiceInstanceDao serviceInstance);
        Task<ServiceInstanceDao[]> FindByServiceDefinitionAsync(string serviceDefinition);
        Task<ServiceInstanceDao> GetAsync(string id);
        Task RemoveAsync(string id);
    }
}
