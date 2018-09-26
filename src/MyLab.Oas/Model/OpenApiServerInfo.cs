using System.Collections.Generic;

namespace MyLab.Oas.Model
{
    /// <summary>
    /// Provide connectivity information to a target server.
    /// </summary>
    public class OpenApiServerInfo
    {
        /// <summary>
        /// REQUIRED. A URL to the target host. This URL supports Server Variables and MAY be relative, to indicate that the host location is relative to the location where the OpenAPI document is being served. Variable substitutions will be made when a variable is named in {brackets}.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// An optional string describing the host designated by the URL. CommonMark syntax MAY be used for rich text representation.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// A map between a variable name and its value. The value is used for substitution in the server's URL template.
        /// </summary>
        public Dictionary<string, OpenApiServerVariable> Variables { get; set; }
    }
}