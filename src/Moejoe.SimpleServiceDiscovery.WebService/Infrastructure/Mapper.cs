using MoeJoe.SimpleServiceDiscovery.Models;

namespace Moejoe.SimpleServiceDiscovery.WebService.Infrastructure
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
