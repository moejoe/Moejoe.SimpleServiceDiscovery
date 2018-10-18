using Newtonsoft.Json;

namespace Moejoe.SimpleServiceDiscovery.Server.Logging
{
    public static class LoggingHelper
    {
        public static string Serialize<TModel>(TModel logObject)
        {
            return JsonConvert.SerializeObject(logObject, Formatting.Indented);
        }
    }
}
