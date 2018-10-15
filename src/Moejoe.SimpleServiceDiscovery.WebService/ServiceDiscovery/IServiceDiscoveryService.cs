using System.Threading.Tasks;

namespace Moejoe.SimpleServiceDiscovery.WebService.ServiceDiscovery
{
    public interface IServiceDiscoveryService
    {
        Task<ServiceDiscoveryResult> DiscoverAsync(string serviceDefinition);
    }
}