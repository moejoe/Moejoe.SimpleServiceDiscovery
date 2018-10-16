using System;

namespace Moejoe.SimpleServiceDiscovery.Server.ServiceRegistration
{
    [Serializable]
    public class ServiceInstanceNotFoundException : Exception
    {
        private const string ErrorMessageTemplate = "The ServiceInstance with the id '{0}' does not exist";
        public ServiceInstanceNotFoundException(string serviceInstanceId) : base(string.Format(ErrorMessageTemplate, serviceInstanceId))
        {

        }
    }

    [Serializable]
    public class ServiceInstanceAlreadyExistsException : Exception
    {
        private const string ErrorMessageTemplate = "Another ServiceInstance with the id '{0}' already exists.";
        public ServiceInstanceAlreadyExistsException(string serviceInstanceId) : base(string.Format(ErrorMessageTemplate, serviceInstanceId))
        {

        }
    }
}
