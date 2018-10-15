using System;

namespace Moejoe.SimpleServiceDiscovery.Client
{
    public static class DiscoveryApi
    {
        public static class Resources
        {
            public const string Services = "Services";
        }
        public static class Discover
        {
            public static Uri DiscoverService(string serviceDefinition) => new Uri($"{Resources.Services}/{serviceDefinition}", UriKind.Relative);
        }

        public static class ErrorTypes
        {
            private const string ErrorRoot = "https://errors.moejoe.com/simpleServiceDiscovery";
            public const string InvalidArgumentType = ErrorRoot + "/InvalidArgumentType";
            public const string ServiceNotFound = ErrorRoot + "/Discovery/ServiceNotFoundType";
        }
    }
}