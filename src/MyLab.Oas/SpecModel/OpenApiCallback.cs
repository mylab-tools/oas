using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace MyLab.Oas.SpecModel
{
    /// <summary>
    /// A map of possible out-of band callbacks related to the parent operation. Each value in the map is a Path Item Object that describes a set of requests that may be initiated by the API provider and the expected responses. The key value used to identify the callback object is an expression, evaluated at runtime, that identifies a URL to use for the callback operation.
    /// </summary>
    public class OpenApiCallback : Dictionary<string, OpenApiPath>
    {
        /// <summary>
        /// Link
        /// </summary>
        [YamlMember(Alias = "$ref")]
        public string Ref { get; set; }
    }
}