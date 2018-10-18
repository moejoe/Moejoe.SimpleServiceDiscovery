using Moejoe.SimpleServiceDiscovery.Common.Models;
using Moejoe.SimpleServiceDiscovery.Server.ServiceDiscovery;
using System;
using System.Collections.Generic;
using System.Text;

namespace Moejoe.SimpleServiceDiscovery.Server.Logging
{
    public class ServiceDiscoveryResultLog
    {
        public ServiceDiscoveryResultLog(ServiceDiscoveryResult result)
        {
            Instances = result.Instances;
        }
        public ServiceInstance[] Instances { get; }

        public override string ToString()
        {
            return LoggingHelper.Serialize(this);
        }
    }

    public class ServiceInstanceLog
    {
        public ServiceInstanceLog(ServiceInstance instance)
        {
            BaseUrl = instance.BaseUrl;
            Id = instance.Id;
            ServiceDefinition = instance.ServiceDefinition;
        }

        public string BaseUrl { get; }
        public string Id { get; }
        public string ServiceDefinition { get; }

        public override string ToString()
        {
            return LoggingHelper.Serialize(this);
        }
    }

    
}
