using MoeJoe.SimpleServiceDiscovery.Models;

namespace Moejoe.SimpleServiceDiscovery.Client
{
    public interface IRegistrationClient
    {
        RegistrationResponse Register(RestServiceInstance definition);
    }
}
