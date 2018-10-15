using MoeJoe.SimpleServiceDiscovery.Models;
using System.Threading.Tasks;

namespace Moejoe.SimpleServiceDiscovery.WebService.ServiceRegistration
{
    public interface IServiceRegistrationService
    {
        Task<ServiceRegistrationResult> RegisterAsync(ServiceInstance service);
    }
}
