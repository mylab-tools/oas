using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace MyLab.Oas.Model
{
    /// <summary>
    /// Describes WebApi
    /// </summary>
    public class ApiDescription
    {
        /// <summary>
        /// REQUIRED. This string MUST be the semantic version number of the OpenAPI Specification version that the OpenAPI document uses. The openapi field SHOULD be used by tooling specifications and clients to interpret the OpenAPI document. This is not related to the API info.version string.
        /// </summary>
        [YamlMember(Alias = "openapi")]
        public string OpenApi { get; set; }

        /// <summary>
        /// REQUIRED. Provides metadata about the API. The metadata MAY be used by tooling as required.
        /// </summary>
        public OpenApiInfo Info { get; set; }

        /// <summary>
        /// An array of Server Objects, which provide connectivity information to a target server. If the servers property is not provided, or is an empty array, the default value would be a Server Object with a url value of /.
        /// </summary>
        public OpenApiServerInfo[] Servers { get; set; }

        /// <summary>
        /// REQUIRED. The available paths and operations for the API.
        /// </summary>
        public Dictionary<string,OpenApiPath> Paths { get; set; }

        /// <summary>
        /// An element to hold various schemas for the specification.
        /// </summary>
        public OpenApiComponents Components { get; set; }

        /// <summary>
        /// A declaration of which security mechanisms can be used across the API. The list of values includes alternative security requirement objects that can be used. Only one of the security requirement objects need to be satisfied to authorize a request. Individual operations can override this definition.
        /// </summary>
        public OpenApiSecurityRequirements[] Security { get; set; }

        /// <summary>
        /// A list of tags used by the specification with additional metadata. The order of the tags can be used to reflect on their order by the parsing tools. Not all tags that are used by the Operation Object must be declared. The tags that are not declared MAY be organized randomly or based on the tools' logic. Each tag name in the list MUST be unique.
        /// </summary>
        public OpenApiTag[] Tags { get; set; }

        /// <summary>
        /// Additional external documentation.
        /// </summary>
        public OpenApiExtranalDoc ExternalDocs { get; set; }
    }
}