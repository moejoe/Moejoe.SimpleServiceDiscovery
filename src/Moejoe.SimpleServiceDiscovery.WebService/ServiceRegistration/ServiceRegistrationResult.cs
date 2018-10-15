namespace Moejoe.SimpleServiceDiscovery.WebService.ServiceRegistration
{
    public class ServiceRegistrationResult
    {
        public bool IsError { get; set; }
        public static ServiceRegistrationResult Success => new ServiceRegistrationResult { IsError = false };
    }


}
