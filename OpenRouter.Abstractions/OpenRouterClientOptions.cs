namespace OpenRouter.Abstractions
{
    /// <summary>
    /// Root options for configuring the OpenRouter client.
    /// </summary>
    public class OpenRouterClientOptions : IValidatableOptions
    {
        /// <summary>
        /// Gets or sets the authentication options.
        /// </summary>
        public AuthenticationOptions Authentication { get; set; } = new AuthenticationOptions();

        /// <summary>
        /// Gets or sets the HTTP configuration options.
        /// </summary>
        public HttpOptions Http { get; set; } = new HttpOptions();

        /// <summary>
        /// Gets or sets the serialization options.
        /// </summary>
        public SerializationOptions Serialization { get; set; } = new SerializationOptions();

        /// <summary>
        /// Gets or sets the resilience options.
        /// </summary>
        public ResilienceOptions Resilience { get; set; } = new ResilienceOptions();

        /// <summary>
        /// Gets or sets the provider routing options.
        /// </summary>
        public ProviderRoutingOptions ProviderRouting { get; set; } = new ProviderRoutingOptions();

        /// <inheritdoc/>
        public void Validate()
        {
            Authentication?.Validate();
            Http?.Validate();
            Serialization?.Validate();
            Resilience?.Validate();
            ProviderRouting?.Validate();
        }
    }
}
