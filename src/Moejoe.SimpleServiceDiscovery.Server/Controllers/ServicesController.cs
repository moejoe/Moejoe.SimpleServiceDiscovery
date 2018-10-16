using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moejoe.SimpleServiceDiscovery.Common;
using Moejoe.SimpleServiceDiscovery.Common.Models;
using Moejoe.SimpleServiceDiscovery.Server.Errors;
using Moejoe.SimpleServiceDiscovery.Server.ServiceDiscovery;
using Moejoe.SimpleServiceDiscovery.Server.ServiceRegistration;

namespace Moejoe.SimpleServiceDiscovery.Server.Controllers
{
    [Route(DiscoveryApi.Resources.Services)]
    public class ServicesController : ControllerBase
    {
        private readonly IServiceDiscoveryService _serviceDiscoveryService;
        private readonly IServiceRegistrationService _serviceRegistrationService;

        public ServicesController(IServiceDiscoveryService serviceDiscoveryService, IServiceRegistrationService serviceRegistrationService)
        {
            _serviceDiscoveryService = serviceDiscoveryService ?? throw new ArgumentNullException(nameof(serviceDiscoveryService));
            _serviceRegistrationService = serviceRegistrationService ?? throw new ArgumentNullException(nameof(serviceRegistrationService));
        }

        [HttpGet("{serviceDefinition}", Name = nameof(DiscoverService))]
        [ProducesResponseType(200, Type = typeof(ServiceInstance[]))]
        [ProducesResponseType(400, Type = typeof(Error))]
        [ProducesResponseType(404, Type = typeof(Error))]
        public async Task<IActionResult> DiscoverService(string serviceDefinition)
        {
            if (string.IsNullOrWhiteSpace(serviceDefinition))
            {
                return BadRequest(this.InvalidArgumentError(nameof(serviceDefinition), "Must not be null or whitespace."));
            }

            var result = await _serviceDiscoveryService.DiscoverAsync(serviceDefinition);
            if (result.ServiceNotFound())
            {
                return NotFound(this.GenerateError(DiscoveryApi.ErrorTypes.ServiceNotFound, err =>
                {
                    err.Title = "The requested service could not be found.";
                }));
            }
            return Ok(result.Instances);
        }
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400, Type = typeof(Error))]
        public async Task<IActionResult> RegisterService([FromBody] ServiceInstance model)
        {
            if (model == null)
            {
                return BadRequest(this.InvalidArgumentError(nameof(model), "Must not be null"));
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(this.InvalidArgumentError(nameof(model), ModelState));
            }
            try
            {
                await _serviceRegistrationService.RegisterAsync(model);
                return CreatedAtRoute(nameof(DiscoverService), new { serviceDefinition = model.ServiceDefinition }, null);
            }
            catch (ServiceInstanceAlreadyExistsException)
            {
                return BadRequest(this.GenerateError(DiscoveryApi.ErrorTypes.ServiceAlreadyExists, error =>
                {
                    error.Title = "Another ServiceInstance with the same id already exists.";
                    error.Detail = "Please ensure that each ServiceInstance id is unique!";
                }));
            }
        }
        [HttpDelete("{id}", Name = nameof(UnregisterService))]
        public async Task<IActionResult> UnregisterService(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return BadRequest(this.InvalidArgumentError(nameof(id), "Must not be empty or null"));
            }
            try
            {
                await _serviceRegistrationService.UnregisterAsync(id);
                return NoContent();
            }
            catch (ServiceInstanceNotFoundException)
            {
                return NotFound(this.GenerateError(DiscoveryApi.ErrorTypes.ServiceNotFound, error =>
                {
                    error.Title = "The ServiceInstance does not exist.";
                }));
            }
        }
    }

}
