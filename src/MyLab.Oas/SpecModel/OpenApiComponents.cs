using System.Collections.Generic;

namespace MyLab.Oas.SpecModel
{
    /// <summary>
    /// An element to hold various schemas for the specification.
    /// </summary>
    public class OpenApiComponents
    {
        /// <summary>
        /// Schemas
        /// </summary>
        public Dictionary<string, OpenApiSchema> Schemas { get; set; }
        /// <summary>
        /// Responses
        /// </summary>
        public Dictionary<string, OpenApiResponse> Responses { get; set; }
        /// <summary>
        /// Parameters
        /// </summary>
        public Dictionary<string, OpenApiParameter> Parameters { get; set; }
        /// <summary>
        /// Examples
        /// </summary>
        public Dictionary<string, OpenApiExample> Examples { get; set; }
        /// <summary>
        /// RequestContents
        /// </summary>
        public Dictionary<string, OpenApiRequestBody> RequestBodies { get; set; }
        /// <summary>
        /// Security schemes
        /// </summary>
        public Dictionary<string, OpenApiSecurityScheme> SecuritySchemes { get; set; }
        /// <summary>
        /// Links
        /// </summary>
        public Dictionary<string, OpenApiLink> Links { get; set; }
        /// <summary>
        /// Callbacks
        /// </summary>
        public Dictionary<string, OpenApiCallback> Callbacks { get; set; }
    }
}