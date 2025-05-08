namespace OpenRouter.Abstractions
{
    /// <summary>
    /// Provides configuration options for the OpenRouter client.
    /// </summary>
    public interface IOpenRouterConfiguration
    {
        /// <summary>
        /// Gets the base URL for the OpenRouter API.
        /// </summary>
        string BaseUrl { get; }
        // Add other configuration properties as needed (e.g., timeouts, auth, etc.)
    }
}
