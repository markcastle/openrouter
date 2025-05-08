namespace OpenRouter.Abstractions
{
    /// <summary>
    /// Options for provider routing configuration.
    /// </summary>
    public class ProviderRoutingOptions : IValidatableOptions
    {
        /// <summary>
        /// Gets or sets the preferred provider name (if any).
        /// </summary>
        public string? PreferredProvider { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to enable provider fallback.
        /// </summary>
        public bool EnableFallback { get; set; } = true;

        /// <inheritdoc/>
        public void Validate()
        {
            // No required fields for now; add validation as needed
        }
    }
}
