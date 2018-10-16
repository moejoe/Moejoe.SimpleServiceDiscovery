using System.ComponentModel.DataAnnotations;

namespace Moejoe.SimpleServiceDiscovery.Storage.Models
{
    public class ServiceInstanceDao
    {
        [Key]
        [MaxLength(256)]
        public string Id { get; set; }
        [MinLength(3)]
        [MaxLength(256)]
        public string ServiceDefinition { get; set; }
        [MinLength(3)]
        [MaxLength(2048)]
        public string BaseUrl { get; set; }
    }
}
