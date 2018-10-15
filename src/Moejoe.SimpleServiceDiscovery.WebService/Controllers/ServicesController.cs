﻿using Microsoft.AspNetCore.Mvc;
using System;
using Moejoe.SimpleServiceDiscovery.WebService.ServiceDiscovery;
using MoeJoe.SimpleServiceDiscovery.Models;
using System.Threading.Tasks;
using Moejoe.SimpleServiceDiscovery.Client;
using Moejoe.SimpleServiceDiscovery.WebService.ServiceRegistration;

namespace Moejoe.SimpleServiceDiscovery.WebService.Controllers
{
    [Route(DiscoveryApi.Resources.Services)]
    public class ServicesController : Controller
    {
        private readonly IServiceDiscoveryService _serviceDiscoveryService;
        private readonly IServiceRegistrationService _serviceRegistrationService;

        public ServicesController(IServiceDiscoveryService serviceDiscoveryService)
        {
            _serviceDiscoveryService = serviceDiscoveryService ?? throw new ArgumentNullException(nameof(serviceDiscoveryService));
        }

        [HttpGet("{serviceDefinition}", Name = "DiscoverService")]
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
        public async Task<IActionResult> RegisterService([FromBody]ServiceInstance model)
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
                return CreatedAtRoute("DiscoverSerivce", new { serviceDefinition = model.ServiceDefinition });
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
    }

}
