﻿using Moejoe.SimpleServiceDiscovery.Common.Models;

namespace Moejoe.SimpleServiceDiscovery.Server.Infrastructure
{
    public static class Mapper
    {
        public static ServiceInstanceDao ToDao(this ServiceInstance src)
        {
            return new ServiceInstanceDao
            {
                ServiceDefinition = src.ServiceDefinition,
                Id = src.Id,
                BaseUrl = src.BaseUrl
            };
        }
    }
}