namespace OpenRouter.Abstractions
{
    /// <summary>
    /// Options for resilience and retry configuration.
    /// </summary>
    public class ResilienceOptions : IValidatableOptions
    {
        /// <summary>
        /// Gets or sets the retry policy type (e.g., "ExponentialBackoff").
        /// </summary>
        public string? RetryPolicy { get; set; } = "ExponentialBackoff";

        /// <summary>
        /// Gets or sets the circuit breaker threshold.
        /// </summary>
        public int CircuitBreakerThreshold { get; set; } = 5;

        /// <summary>
        /// Gets or sets the circuit breaker reset interval in seconds.
        /// </summary>
        public int CircuitBreakerResetSeconds { get; set; } = 60;

        /// <inheritdoc/>
        public void Validate()
        {
            if (CircuitBreakerThreshold < 0)
            {
                throw new OpenRouterException("CircuitBreakerThreshold cannot be negative.");
            }

            if (CircuitBreakerResetSeconds <= 0)
            {
                throw new OpenRouterException("CircuitBreakerResetSeconds must be positive.");
            }
        }
    }
}
