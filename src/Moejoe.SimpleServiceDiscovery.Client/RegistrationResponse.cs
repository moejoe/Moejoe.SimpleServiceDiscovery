namespace Moejoe.SimpleServiceDiscovery.Client
{
    public class RegistrationResponse
    {
        public bool IsError { get; set; }
        public static RegistrationResponse SuccessResponse => new RegistrationResponse { IsError = false };
        public static RegistrationResponse ErrorResponse => new RegistrationResponse { IsError = true };
    }
}
