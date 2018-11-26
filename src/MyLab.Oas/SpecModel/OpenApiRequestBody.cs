using System.Collections.Generic;

namespace MyLab.Oas.SpecModel
{
    /// <summary>
    /// Describes a single request body.
    /// </summary>
    public class OpenApiRequestBody : OpenApiRefObject
    {
        /// <summary>
        /// A brief description of the request body. This could contain examples of use. CommonMark syntax MAY be used for rich text representation.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// REQUIRED. The content of the request body. The key is a media type or media type range and the value describes it. For requests that match multiple keys, only the most specific key is applicable. e.g. text/plain overrides text/*
        /// </summary>
        public Dictionary<string, OpenApiMediaType> Content { get; set; }

        /// <summary>
        /// Determines if the request body is required in the request. Defaults to false.
        /// </summary>
        public bool Required { get; set; }
    }
}