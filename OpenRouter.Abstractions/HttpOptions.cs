namespace OpenRouter.Abstractions
{
    /// <summary>
    /// Options for HTTP configuration.
    /// </summary>
    public class HttpOptions : IValidatableOptions
    {
        /// <summary>
        /// Gets or sets the base URL for the API.
        /// </summary>
        public string? BaseUrl { get; set; }

        /// <summary>
        /// Gets or sets the request timeout in seconds.
        /// </summary>
        public int TimeoutSeconds { get; set; } = 100;

        /// <summary>
        /// Gets or sets the maximum number of retries for transient errors.
        /// </summary>
        public int MaxRetries { get; set; } = 3;

        /// <inheritdoc/>
        public void Validate()
        {
            if (string.IsNullOrWhiteSpace(BaseUrl))
            {
                throw new OpenRouterException("BaseUrl must be provided in HttpOptions.");
            }

            if (TimeoutSeconds <= 0)
            {
                throw new OpenRouterException("TimeoutSeconds must be positive.");
            }

            if (MaxRetries < 0)
            {
                throw new OpenRouterException("MaxRetries cannot be negative.");
            }
        }
    }
}
