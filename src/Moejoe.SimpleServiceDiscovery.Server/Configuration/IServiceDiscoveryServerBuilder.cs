using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace Moejoe.SimpleServiceDiscovery.Server.Configuration
{
    public interface IServiceDiscoveryServerBuilder
    {
        IServiceCollection Services { get; }
    }
}
