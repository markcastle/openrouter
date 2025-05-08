namespace OpenRouter.Abstractions
{
    /// <summary>
    /// Options for authentication configuration.
    /// </summary>
    public class AuthenticationOptions : IValidatableOptions
    {
        /// <summary>
        /// Gets or sets the API key for authenticating requests.
        /// </summary>
        public string? ApiKey { get; set; }

        /// <summary>
        /// Gets or sets the bearer token for authentication.
        /// </summary>
        public string? BearerToken { get; set; }

        /// <summary>
        /// Gets or sets the authentication scheme ("ApiKey", "Bearer", etc).
        /// </summary>
        public string? Scheme { get; set; }

        /// <inheritdoc/>
        public void Validate()
        {
            // At least one authentication method must be provided
            if (string.IsNullOrWhiteSpace(ApiKey) && string.IsNullOrWhiteSpace(BearerToken))
            {
                throw new OpenRouterAuthenticationException("Either ApiKey or BearerToken must be provided.");
            }
        }
    }
}
