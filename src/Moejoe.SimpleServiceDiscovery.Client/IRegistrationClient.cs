
using Moejoe.SimpleServiceDiscovery.Common.Models;
using System.Threading;
using System.Threading.Tasks;

namespace Moejoe.SimpleServiceDiscovery.Client
{
    public interface IRegistrationClient
    {
        Task<RegistrationResponse> RegisterAsync(ServiceInstance definition, CancellationToken cancellationToken = default(CancellationToken));
        Task UnregisterAsync(string id, CancellationToken cancellationToken = default(CancellationToken));
    }
}
