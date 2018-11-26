namespace MyLab.Oas.SpecModel
{
    /// <summary>
    /// Allows configuration of the supported OAuth Flows.
    /// </summary>
    public class OpenApiOAuthFlows
    {
        /// <summary>
        /// Configuration for the OAuth Implicit flow
        /// </summary>
        public OpenApiOAuthFlow Implicit { get; set; }
        /// <summary>
        /// Configuration for the OAuth Resource Owner Password flow
        /// </summary>
        public OpenApiOAuthFlow Password { get; set; }
        /// <summary>
        /// Configuration for the OAuth Client Credentials flow. Previously called application in OpenAPI 2.0.
        /// </summary>
        public OpenApiOAuthFlow ClientCredentials { get; set; }
        /// <summary>
        /// Configuration for the OAuth Authorization Code flow. Previously called accessCode in OpenAPI 2.0.
        /// </summary>
        public OpenApiOAuthFlow AuthorizationCode { get; set; }
    }
}