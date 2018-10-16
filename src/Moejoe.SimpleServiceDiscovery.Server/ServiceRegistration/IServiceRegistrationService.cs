using System.Threading.Tasks;
using Moejoe.SimpleServiceDiscovery.Common.Models;

namespace Moejoe.SimpleServiceDiscovery.Server.ServiceRegistration
{
    public interface IServiceRegistrationService
    {
        Task<ServiceRegistrationResult> RegisterAsync(ServiceInstance service);
        Task UnregisterAsync(string id);
    }
}
