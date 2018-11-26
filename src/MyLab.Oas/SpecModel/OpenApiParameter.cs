using System.Collections.Generic;

namespace MyLab.Oas.SpecModel
{
    /// <summary>
    /// Describes a single operation parameter. A unique parameter is defined by a combination of a name and location.
    /// </summary>
    public class OpenApiParameter : OpenApiRefObject
    {
        /// <summary>
        /// REQUIRED. The name of the parameter. Parameter names are case sensitive.
        /// * If 'in' is "path", the name field MUST correspond to the associated path segment from the path field in the Paths Object.See Path Templating for further information.
        /// * If 'in' is "header" and the name field is "Accept", "Content-Type" or "Authorization", the parameter definition SHALL be ignored.
        /// * For all other cases, the name corresponds to the parameter name used by the 'in' property.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// REQUIRED. The location of the parameter. Possible values are "query", "header", "path" or "cookie".
        /// </summary>
        public string In { get; set; }
        /// <summary>
        /// A brief description of the parameter. This could contain examples of use. CommonMark syntax MAY be used for rich text representation.
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Determines whether this parameter is mandatory. If the parameter location is "path", this property is REQUIRED and its value MUST be true. Otherwise, the property MAY be included and its default value is false.
        /// </summary>
        public bool Required { get; set; }
        /// <summary>
        /// Specifies that a parameter is deprecated and SHOULD be transitioned out of usage.
        /// </summary>
        public bool Deprecated { get; set; }
        /// <summary>
        /// Sets the ability to pass empty-valued parameters. This is valid only for query parameters and allows sending a parameter with an empty value. Default value is false. If style is used, and if behavior is n/a (cannot be serialized), the value of allowEmptyValue SHALL be ignored.
        /// </summary>
        public bool AllowEmptyValue { get; set; }
        /// <summary>
        /// Describes how the parameter value will be serialized depending on the type of the parameter value. Default values (based on value of in): for query - form; for path - simple; for header - simple; for cookie - form.
        /// </summary>
        public string Style { get; set; }
        /// <summary>
        /// When this is true, parameter values of type array or object generate separate parameters for each value of the array or key-value pair of the map. For other types of parameters this property has no effect. When style is form, the default value is true. For all other styles, the default value is false.
        /// </summary>
        public bool Explode { get; set; }
        /// <summary>
        /// Determines whether the parameter value SHOULD allow reserved characters, as defined by RFC3986 :/?#[]@!$&'()*+,;= to be included without percent-encoding. This property only applies to parameters with an in value of query. The default value is false.
        /// </summary>
        public bool AllowReserved { get; set; }
        /// <summary>
        /// The schema defining the type used for the parameter.
        /// </summary>
        public OpenApiSchema Schema { get; set; }
        /// <summary>
        /// Example of the media type. The example SHOULD match the specified schema and encoding properties if present. The example object is mutually exclusive of the examples object. Furthermore, if referencing a schema which contains an example, the example value SHALL override the example provided by the schema. To represent examples of media types that cannot naturally be represented in JSON or YAML, a string value can contain the example with escaping where necessary.
        /// </summary>
        public string Example { get; set; }
        /// <summary>
        /// Examples of the media type. Each example SHOULD contain a value in the correct format as specified in the parameter encoding. The examples object is mutually exclusive of the example object. Furthermore, if referencing a schema which contains an example, the examples value SHALL override the example provided by the schema.
        /// </summary>
        public Dictionary<string, OpenApiExample> Examples { get; set; }
        /// <summary>
        /// A map containing the representations for the parameter. The key is the media type and the value describes it. The map MUST only contain one entry.
        /// </summary>
        public Dictionary<string, OpenApiMediaType> Content { get; set; }
    }
}