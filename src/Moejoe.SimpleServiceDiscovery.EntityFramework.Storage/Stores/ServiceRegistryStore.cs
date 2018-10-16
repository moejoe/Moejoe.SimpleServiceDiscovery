using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Moejoe.SimpleServiceDiscovery.EntityFramework.Storage.DbContexts;
using Moejoe.SimpleServiceDiscovery.Storage.Models;
using Moejoe.SimpleServiceDiscovery.Storage.Stores;

namespace Moejoe.SimpleServiceDiscovery.EntityFramework.Storage.Stores
{
    public class ServiceRegistryStore : IServiceRegistryStore
    {
        private readonly ServiceDiscoveryContext _context;

        public ServiceRegistryStore(ServiceDiscoveryContext context)
        {
            _context = context;
        }

        public Task<ServiceInstanceDao[]> FindByServiceDefinitionAsync(string serviceDefinition)
        {
            return _context.ServiceInstances
                .AsNoTracking()
                .Where(p => p.ServiceDefinition == serviceDefinition)
                .ToArrayAsync();
        }

        public Task<ServiceInstanceDao> GetAsync(string id)
        {
            return _context.ServiceInstances
                .AsNoTracking()
                .SingleOrDefaultAsync(p => p.Id == id);
        }

        public async Task RemoveAsync(string id)
        {
            var instance = await _context.ServiceInstances.SingleOrDefaultAsync(p => p.Id == id);
            if (instance == null)
            {
                return;
            }
            _context.ServiceInstances.Remove(instance);
            await _context.SaveChangesAsync();
        }

        public async Task StoreAsync(ServiceInstanceDao serviceInstance)
        {
            var instance = await _context.ServiceInstances.SingleOrDefaultAsync(p => p.Id == serviceInstance.Id);
            if (instance == null)
            {
                _context.ServiceInstances.Add(serviceInstance);
            }
            else
            {
                instance.Id = serviceInstance.Id;
                instance.ServiceDefinition = serviceInstance.ServiceDefinition;
                instance.BaseUrl = serviceInstance.BaseUrl;
            }

            await _context.SaveChangesAsync();
        }
    }
}
