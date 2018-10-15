using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Moejoe.SimpleServiceDiscovery.Client;
using MoeJoe.SimpleServiceDiscovery.Models;

namespace Moejoe.SimpleServiceDiscovery.WebService
{
    public static class ControllerExtensions
    {
        public static Error InvalidArgumentError(this ControllerBase ctrl, string argumentName, string errorMessage)
        {
            return ctrl.GenerateError(err =>
            {
                err.Type = DiscoveryApi.ErrorTypes.InvalidArgumentType;
                err.Title = $"Invalid Request Parameters supplied: {argumentName}.";
                err.Detail = errorMessage;
            });
        }

        internal static Error GenerateError(this ControllerBase ctrl, Action<Error> configureError)
        {
            var error = new Error
            {
                Instance = ctrl.Request.GetDisplayUrl()
            };
            configureError(error);
            return error;
        }
    }
}
