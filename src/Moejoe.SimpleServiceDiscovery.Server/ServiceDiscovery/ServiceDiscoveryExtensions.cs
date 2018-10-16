using System.Linq;

namespace Moejoe.SimpleServiceDiscovery.Server.ServiceDiscovery
{
    public static class ServiceDiscoveryExtensions
    {
        public static bool ServiceNotFound(this ServiceDiscoveryResult result)
        {
            return !result.Instances.Any();
        }
    }

}
