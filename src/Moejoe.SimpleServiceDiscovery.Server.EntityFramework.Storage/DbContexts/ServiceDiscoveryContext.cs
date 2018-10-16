using Microsoft.EntityFrameworkCore;
using Moejoe.SimpleServiceDiscovery.Server.Storage.Models;

namespace Moejoe.SimpleServiceDiscovery.Server.EntityFramework.Storage.DbContexts
{
    public class ServiceDiscoveryContext : DbContext
    {
        public ServiceDiscoveryContext(DbContextOptions<ServiceDiscoveryContext> options) : base(options)
        {
            
        }
        public DbSet<ServiceInstanceDao> ServiceInstances { get; set; }
    }
}
