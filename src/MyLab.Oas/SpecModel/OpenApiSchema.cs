using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace MyLab.Oas.SpecModel
{
    /// <summary>
    /// The Schema Object allows the definition of input and output data types. These types can be objects, but also primitives and arrays. This object is an extended subset of the JSON Schema Specification Wright Draft 00.
    /// For more information about the properties, see JSON Schema Core and JSON Schema Validation.Unless stated otherwise, the property definitions follow the JSON Schema.
    /// </summary>
    public class OpenApiSchema : OpenApiRefObject
    {
        /// <summary>
        /// Value MUST be a string. Multiple types via an array are not supported.
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// See Data Type Formats for further details. While relying on JSON Schema's defined formats, the OAS offers a few additional predefined formats.
        /// </summary>
        public string Format { get; set; }
        /// <summary>
        /// Example
        /// </summary>
        public object Example { get; set; }

        /// <summary>
        /// CommonMark syntax MAY be used for rich text representation.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// List of required properties
        /// </summary>
        public string[] Required { get; set; }

        /// <summary>
        /// Property definitions MUST be a Schema Object and not a standard JSON Schema (inline or referenced).
        /// </summary>
        public Dictionary<string, OpenApiSchema> Properties { get; set; }

        /// <summary>
        /// Value MUST be an object and not an array. Inline or referenced schema MUST be of a Schema Object and not a standard JSON Schema. items MUST be present if the type is array.
        /// </summary>
        public OpenApiSchema Items{ get; set; }
        /// <summary>
        /// Contains descriptions for enum items
        /// </summary>
        [YamlMember(Alias = "x-enum-description")]
        public XEnumItemDescription[] XEnumDescription { get; set; }
        /// <summary>
        /// Enumerate available values
        /// </summary>
        public string[] Enum { get; set; }
        /// <summary>
        /// Inline or referenced schema MUST be of a Schema Object and not a standard JSON Schema.
        /// </summary>
        public OpenApiSchema[] AllOf { get; set; }
        /// <summary>
        /// Inline or referenced schema MUST be of a Schema Object and not a standard JSON Schema.
        /// </summary>
        public OpenApiSchema[] OneOf { get; set; }
        /// <summary>
        /// Inline or referenced schema MUST be of a Schema Object and not a standard JSON Schema.
        /// </summary>
        public OpenApiSchema[] AnyOf { get; set; }
        /// <summary>
        /// Inline or referenced schema MUST be of a Schema Object and not a standard JSON Schema.
        /// </summary>
        public OpenApiSchema[] Not { get; set; }
        /// <summary>
        ///  The default value represents what would be assumed by the consumer of the input as the value of the schema if one is not provided. Unlike JSON Schema, the value MUST conform to the defined type for the Schema Object defined at the same level. For example, if type is string, then default can be "foo" but cannot be 1.
        /// </summary>
        public object Default { get; set; }
        /// <summary>
        /// Value can be boolean or object. Inline or referenced schema MUST be of a Schema Object and not a standard JSON Schema.
        /// </summary>
        public OpenApiSchema AdditionalProperties { get; set; }
    }
}