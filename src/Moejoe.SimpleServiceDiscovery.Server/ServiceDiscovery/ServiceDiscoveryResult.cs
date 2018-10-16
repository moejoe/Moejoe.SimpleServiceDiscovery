using Moejoe.SimpleServiceDiscovery.Common.Models;

namespace Moejoe.SimpleServiceDiscovery.Server.ServiceDiscovery
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