using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Moejoe.SimpleServiceDiscovery.WebService.Infrastructure
{
    public class ServiceDiscoveryContext : DbContext
    {
        public ServiceDiscoveryContext(DbContextOptions<ServiceDiscoveryContext> options) : base(options)
        {
            
        }
        public DbSet<ServiceInstanceDao> ServiceInstances { get; set; }
    }

    public class ServiceInstanceDao
    {
        [Key]
        [MaxLength(256)]
        public string Id { get; set; }
        [MinLength(3)]
        [MaxLength(256)]
        public string ServiceDefinition { get; set; }
        [MinLength(3)]
        [MaxLength(2048)]
        public string BaseUrl { get; internal set; }
    }
}
