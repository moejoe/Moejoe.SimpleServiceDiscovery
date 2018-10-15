using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace MoeJoe.SimpleServiceDiscovery.Models
{
    [DataContract]
    public class ServiceInstance
    {
        /// <summary>
        ///     Specifies the logical name of the service. Many service instances may share the same logical service name.
        /// </summary>
        /// <example>
        ///     example.com.myApi
        /// </example>
        [DataMember(IsRequired = true)]
        [Required(AllowEmptyStrings = false)]
        public string ServiceDefinition { get; set; }
        /// <summary>
        ///     Specifies a unique ID for this service. This must be unique per agent. This defaults to the Name parameter if not provided.
        /// </summary>
        /// <example>
        ///     myApi@appserver003
        /// </example>
        [DataMember(IsRequired = true)]
        [Required(AllowEmptyStrings = false)]
        public string Id { get; set; }
        /// <summary>
        ///     Base Path
        /// </summary>
        /// <example>
        ///     https://appserver003:8190/myApi/
        /// </example>
        [DataMember(IsRequired = true)]
        [Required(AllowEmptyStrings = false)]
        public string BaseUrl { get; set; }
    }
}
