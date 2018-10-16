using Microsoft.EntityFrameworkCore;

namespace Moejoe.SimpleServiceDiscovery.Server.Infrastructure
{
    public class ServiceDiscoveryContext : DbContext
    {
        public ServiceDiscoveryContext(DbContextOptions<ServiceDiscoveryContext> options) : base(options)
        {
            
        }
        public DbSet<ServiceInstanceDao> ServiceInstances { get; set; }
    }
}
