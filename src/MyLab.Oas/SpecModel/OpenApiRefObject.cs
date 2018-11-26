using YamlDotNet.Serialization;

namespace MyLab.Oas.SpecModel
{
    /// <summary>
    /// May contains object reference
    /// </summary>
    public class OpenApiRefObject 
    {
        /// <summary>
        /// Reference
        /// </summary>
        [YamlMember(Alias = "$ref")]
        public string Ref { get; set; }
    }
}