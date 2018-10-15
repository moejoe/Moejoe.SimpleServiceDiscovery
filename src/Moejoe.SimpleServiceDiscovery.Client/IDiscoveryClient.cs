namespace Moejoe.SimpleServiceDiscovery.Client
{
    public interface IDiscoveryClient
    {
        DiscoveryResponse Discover(string serviceName);
    }
}