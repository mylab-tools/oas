namespace MyLab.Oas.SpecModel
{
    /// <summary>
    /// Allows referencing an external resource for extended documentation.
    /// </summary>
    public class OpenApiExtranalDoc
    {
        /// <summary>
        /// A short description of the target documentation. CommonMark syntax MAY be used for rich text representation.
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// REQUIRED. The URL for the target documentation. Value MUST be in the format of a URL.
        /// </summary>
        public string Url { get; set; }
    }
}