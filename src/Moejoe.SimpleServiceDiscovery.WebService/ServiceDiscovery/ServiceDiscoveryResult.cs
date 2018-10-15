using MoeJoe.SimpleServiceDiscovery.Models;

namespace Moejoe.SimpleServiceDiscovery.WebService.ServiceDiscovery
{
    public class ServiceDiscoveryResult
    {
        public static ServiceDiscoveryResult Empty => new ServiceDiscoveryResult
        {
            Instances = new ServiceInstance[] { }
        };
        public ServiceInstance[] Instances { get;set; }    
    }
}