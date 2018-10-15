using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace MoeJoe.SimpleServiceDiscovery.Models
{
    /// <summary>
    ///     Rfc 7807 Error Type
    /// </summary>
    [DataContract]
    public class Error
    {
        public string Title { get; set; }
        public string Detail { get; set; }
        public string Instance { get; set; }
        /// <summary>
        ///     A URI reference [RFC3986] that identifies the problem type.  
        /// </summary>
        /// <remarks>
        ///     This specification encourages that, when
        ///     dereferenced, it provide human-readable documentation for the
        ///     problem type (e.g., using HTML [W3C.REC-html5-20141028]).  When
        ///     this member is not present, its value is assumed to be "about:blank".
        /// </remarks>>
        [DataMember(IsRequired = true)]
        public string Type { get; set; } = "about:blank";
    }

}
