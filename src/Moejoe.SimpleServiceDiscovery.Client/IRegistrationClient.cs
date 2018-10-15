using MoeJoe.SimpleServiceDiscovery.Models;
using System.Threading;
using System.Threading.Tasks;

namespace Moejoe.SimpleServiceDiscovery.Client
{
    public interface IRegistrationClient
    {
        Task<RegistrationResponse> RegisterAsync(ServiceInstance definition, CancellationToken cancellationToken);
    }
}
