using System.Runtime.Serialization;

namespace MoeJoe.SimpleServiceDiscovery.Models
{
    [DataContract]
    public class ServiceInstance
    {
        [DataMember(IsRequired = true)]
        public string ServiceDefinition { get; set; }
        [DataMember(IsRequired = true)]
        public string Id { get; set; }
        [DataMember(IsRequired = true)]
        public string BaseUrl { get; set; }
    }
}
