namespace MyLab.Oas.SpecModel
{
    /// <summary>
    /// Describes the operations available on a single path. A Path Item MAY be empty, due to ACL constraints. The path itself is still exposed to the documentation viewer but they will not know which operations and parameters are available.
    /// </summary>
    public class OpenApiPath : OpenApiRefObject
    {
        /// <summary>
        /// An optional, string summary, intended to apply to all operations in this path.
        /// </summary>
        public string Summary { get; set; }

        /// <summary>
        /// An optional, string description, intended to apply to all operations in this path. CommonMark syntax MAY be used for rich text representation.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// A definition of a GET operation on this path.
        /// </summary>
        public OpenApiOperation Get { get; set; }
        /// <summary>
        /// A definition of a PUT operation on this path.
        /// </summary>
        public OpenApiOperation Put { get; set; }
        /// <summary>
        /// A definition of a POST operation on this path.
        /// </summary>
        public OpenApiOperation Post { get; set; }
        /// <summary>
        /// A definition of a DELETE operation on this path.
        /// </summary>
        public OpenApiOperation Delete { get; set; }
        /// <summary>
        /// A definition of a OPTIONS operation on this path.
        /// </summary>
        public OpenApiOperation Options { get; set; }
        /// <summary>
        /// A definition of a HEAD operation on this path.
        /// </summary>
        public OpenApiOperation Head { get; set; }
        /// <summary>
        /// A definition of a PATCH operation on this path.
        /// </summary>
        public OpenApiOperation Patch { get; set; }
        /// <summary>
        /// A definition of a TRACE operation on this path.
        /// </summary>
        public OpenApiOperation Trace { get; set; }
        /// <summary>
        /// An alternative server array to service all operations in this path.
        /// </summary>
        public OpenApiServerInfo[] Servers { get; set; }
        /// <summary>
        /// A list of parameters that are applicable for all the operations described under this path. These parameters can be overridden at the operation level, but cannot be removed there. The list MUST NOT include duplicated parameters. A unique parameter is defined by a combination of a name and location. The list can use the Reference Object to link to parameters that are defined at the OpenAPI Object's components/parameters.
        /// </summary>
        public OpenApiParameter[] Parameters { get; set; }
    }
}