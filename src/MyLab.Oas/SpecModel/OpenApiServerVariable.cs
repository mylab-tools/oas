namespace MyLab.Oas.SpecModel
{
    /// <summary>
    /// Web api server variable
    /// </summary>
    public class OpenApiServerVariable
    {
        /// <summary>
        /// An enumeration of string values to be used if the substitution options are from a limited set.
        /// </summary>
        public string[] Enum { get; set; }

        /// <summary>
        /// REQUIRED. The default value to use for substitution, and to send, if an alternate value is not supplied. Unlike the Schema Object's default, this value MUST be provided by the consumer.
        /// </summary>
        public string Default { get; set; }

        /// <summary>
        /// An optional description for the server variable. CommonMark syntax MAY be used for rich text representation.
        /// </summary>
        public string Description { get; set; }
    }
}