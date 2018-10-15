using System;
using System.Linq;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Moejoe.SimpleServiceDiscovery.Client;
using MoeJoe.SimpleServiceDiscovery.Models;

namespace Moejoe.SimpleServiceDiscovery.WebService
{
    public static class ControllerExtensions
    {
        public static Error InvalidArgumentError(this ControllerBase ctrl, string argumentname, ModelStateDictionary modelState)
        {
            return ctrl.GenerateError(DiscoveryApi.ErrorTypes.InvalidArgumentType, err =>
           {
               err.Title = $"Invalid Request Model supplied.";
               err.Detail = string.Join(Environment.NewLine, modelState.Where(p => p.Value.ValidationState == ModelValidationState.Invalid).Select(p => $"{p.Key}: {string.Join(';', p.Value.Errors.Select(q => q.ErrorMessage))}"));
           });
        }
        public static Error InvalidArgumentError(this ControllerBase ctrl, string argumentName, string errorMessage)
        {
            return ctrl.GenerateError(DiscoveryApi.ErrorTypes.InvalidArgumentType, err =>
            {
                err.Title = $"Invalid Request Parameters supplied: {argumentName}.";
                err.Detail = errorMessage;
            });
        }

        internal static Error GenerateError(this ControllerBase ctrl, string errorType, Action<Error> configureError)
        {
            if (errorType == null)
            {
                throw new ArgumentNullException(nameof(errorType));
            }

            var error = new Error
            {
                Type = errorType,
                Instance = ctrl.Request.GetDisplayUrl()
            };
            configureError(error);
            return error;
        }
    }
}
