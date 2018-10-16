using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Moejoe.SimpleServiceDiscovery.Common.Models;
using Moejoe.SimpleServiceDiscovery.Server.Infrastructure;

namespace Moejoe.SimpleServiceDiscovery.Server.ServiceDiscovery
{
    public class ServiceDiscoveryService : IServiceDiscoveryService
    {
        private readonly ServiceDiscoveryContext _context;

        public ServiceDiscoveryService(ServiceDiscoveryContext context)
        {
            _context = context ?? throw new System.ArgumentNullException(nameof(context));
        }

        public async Task<ServiceDiscoveryResult> DiscoverAsync(string serviceDefinition)
        {
            var instances = await _context.ServiceInstances.Where(p => p.ServiceDefinition.Equals(serviceDefinition)).Select(p => new ServiceInstance
            {
                ServiceDefinition = p.ServiceDefinition,
                Id = p.Id,
                BaseUrl = p.BaseUrl

            }).ToArrayAsync();
            return new ServiceDiscoveryResult
            {
                Instances = instances
            };
        }
    }
}
