using YamlDotNet.Serialization;

namespace MyLab.Oas.SpecModel
{
    /// <summary>
    /// A tag used by the specification with additional metadata.
    /// </summary>
    public class OpenApiTag
    {
        /// <summary>
        /// REQUIRED. The name of the tag.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Title
        /// </summary>
        [YamlMember(Alias = "x-title")]
        public string XTitle{ get; set; }

        /// <summary>
        /// A short description for the tag. CommonMark syntax MAY be used for rich text representation.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Additional external documentation for this tag.
        /// </summary>
        public OpenApiExtranalDoc ExternalDocs { get; set; }
    }
}