using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moejoe.SimpleServiceDiscovery.WebService.ServiceDiscovery
{
    public static class ServiceDiscoveryExtensions
    {
        public static bool ServiceNotFound(this ServiceDiscoveryResult result)
        {
            return result.Instances.Any();
        }
    }

}
