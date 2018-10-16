using System.Threading.Tasks;
using Moejoe.SimpleServiceDiscovery.Server.Storage.Models;

namespace Moejoe.SimpleServiceDiscovery.Server.Storage.Stores
{
    public interface IServiceRegistryStore
    {
        Task StoreAsync(ServiceInstanceDao serviceInstance);
        Task<ServiceInstanceDao[]> FindByServiceDefinitionAsync(string serviceDefinition);
        Task<ServiceInstanceDao> GetAsync(string id);
        Task RemoveAsync(string id);
    }
}
