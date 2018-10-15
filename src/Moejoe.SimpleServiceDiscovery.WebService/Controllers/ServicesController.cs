using Microsoft.AspNetCore.Mvc;
using System;
using Moejoe.SimpleServiceDiscovery.WebService.ServiceDiscovery;
using MoeJoe.SimpleServiceDiscovery.Models;
using System.Threading.Tasks;
using Moejoe.SimpleServiceDiscovery.Client;

namespace Moejoe.SimpleServiceDiscovery.WebService.Controllers
{
    [Route(DiscoveryApi.Resources.Services)]
    public class ServicesController : Controller
    {
        private readonly IServiceDiscoveryService _serviceDiscoveryService;

        public ServicesController(IServiceDiscoveryService serviceDiscoveryService)
        {
            _serviceDiscoveryService = serviceDiscoveryService ?? throw new ArgumentNullException(nameof(serviceDiscoveryService));
        }

        [HttpGet("{serviceName}")]
        [ProducesResponseType(200, Type = typeof(ServiceInstance[]))]
        [ProducesResponseType(400, Type = typeof(Error))]
        [ProducesResponseType(404, Type = typeof(Error))]
        public async Task<IActionResult> DiscoverService(string serviceName)
        {
            if (string.IsNullOrWhiteSpace(serviceName))
            {
                return BadRequest(this.InvalidArgumentError(nameof(serviceName), "Must not be null or whitespace."));
            }

            var result = await _serviceDiscoveryService.DiscoverAsync(serviceName);
            if (result.ServiceNotFound())
            {
                return NotFound(this.GenerateError(err =>
                {
                    err.Type = DiscoveryApi.ErrorTypes.ServiceNotFound;
                    err.Title = "The requested service could not be found";
                }));
            }
            return Ok(result.Instances);
        }
        [HttpPost("{serivceName}")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400, Type = typeof(Error))]
        public async Task<IActionResult> RegisterService([FromBody]ServiceInstance model)
        {
            throw new NotImplementedException();
        }
    }

}
