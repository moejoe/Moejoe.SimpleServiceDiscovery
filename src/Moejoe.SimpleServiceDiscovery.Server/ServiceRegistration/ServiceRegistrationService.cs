using System;
using System.Linq;
using System.Threading.Tasks;
using Moejoe.SimpleServiceDiscovery.Common.Models;
using Moejoe.SimpleServiceDiscovery.Server.Infrastructure;

namespace Moejoe.SimpleServiceDiscovery.Server.ServiceRegistration
{

    public class ServiceRegistrationService : IServiceRegistrationService
    {
        private readonly ServiceDiscoveryContext _context;

        public ServiceRegistrationService(ServiceDiscoveryContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<ServiceRegistrationResult> RegisterAsync(ServiceInstance service)
        {
            if (_context.ServiceInstances.Any(p => p.Id == service.Id))
            {
                throw new ServiceInstanceAlreadyExistsException(service.Id);
            }
            _context.ServiceInstances.Add(service.ToDao());
            await _context.SaveChangesAsync();
            return ServiceRegistrationResult.Success;
        }

        public async Task UnregisterAsync(string id)
        {
            var instance = _context.ServiceInstances.SingleOrDefault(p => p.Id == id);
            if (instance == null)
            {
                throw new ServiceInstanceNotFoundException(id);
            }
            _context.ServiceInstances.Remove(instance);
            await _context.SaveChangesAsync();
        }
    }
}
