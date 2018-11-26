using System.Collections.Generic;

namespace MyLab.Oas.SpecModel
{
    /// <summary>
    /// Configuration details for a supported OAuth Flow
    /// </summary>
    public class OpenApiOAuthFlow
    {
        /// <summary>
        /// REQUIRED. The authorization URL to be used for this flow. This MUST be in the form of a URL.
        /// </summary>
        public string AuthorizationUrl { get; set; }
        /// <summary>
        /// REQUIRED. The token URL to be used for this flow. This MUST be in the form of a URL.
        /// </summary>
        public string TokenUrl { get; set; }
        /// <summary>
        /// The URL to be used for obtaining refresh tokens. This MUST be in the form of a URL.
        /// </summary>
        public string RefreshUrl { get; set; }

        /// <summary>
        /// REQUIRED. The available scopes for the OAuth2 security scheme. A map between the scope name and a short description for it.
        /// </summary>
        public Dictionary<string, string> Scopes { get; set; }
    }
}