using System.Threading.Tasks;

namespace Moejoe.SimpleServiceDiscovery.Server.ServiceDiscovery
{
    public interface IServiceDiscoveryService
    {
        Task<ServiceDiscoveryResult> DiscoverAsync(string serviceDefinition);
    }
}