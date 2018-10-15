using System;

namespace Moejoe.SimpleServiceDiscovery.Client
{
    public class RestServiceInstance
    {
        /// <summary>
        ///     Specifies the logical name of the service. Many service instances may share the same logical service name.
        /// </summary>
        /// <example>
        ///     example.com.myApi
        /// </example>
        public string Name { get; set; }

        /// <summary>
        ///     Specifies a unique ID for this service. This must be unique per agent. This defaults to the Name parameter if not provided.
        /// </summary>
        /// <example>
        ///     myApi@appserver003
        /// </example>
        public string Id { get; set; }

        /// <summary>
        ///     Base Path
        /// </summary>
        /// <example>
        ///     https://appserver003:8190/myApi/
        /// </example>
        public string BaseUrl { get; set; }

        /// <summary>
        ///     ApiVersion
        /// </summary>
        /// <example>
        ///     v1.0
        /// </example>
        public string ApiVersion { get; set; }

        /// <summary>
        ///     Assembly / FileVersion of the service.
        /// </summary>
        /// <example>
        ///     1.3.1.100
        /// </example>
        public Version ServiceVersion { get; set; }

        /// <summary>
        ///     PreReleaseIdentifier of the Service
        /// </summary>
        /// <example>
        ///     development
        /// </example>
        public string PreReleaseIdentifier { get; set; }
    }





}
