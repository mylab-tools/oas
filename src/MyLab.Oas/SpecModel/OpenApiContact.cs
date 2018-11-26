﻿namespace MyLab.Oas.SpecModel
{
    /// <summary>
    /// The contact information for the exposed API.
    /// </summary>
    public class OpenApiContact
    {
        /// <summary>
        /// The identifying name of the contact person/organization.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// The URL pointing to the contact information. MUST be in the format of a URL.
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// The email address of the contact person/organization. MUST be in the format of an email address.
        /// </summary>
        public string Email { get; set; }
    }
}