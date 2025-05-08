namespace OpenRouter.Abstractions
{
    /// <summary>
    /// Factory for creating HTTP client adapters.
    /// </summary>
    public interface IHttpClientFactory
    {
        /// <summary>
        /// Creates a new HTTP client adapter instance.
        /// </summary>
        /// <returns>The HTTP client adapter.</returns>
        IHttpClientAdapter CreateClient();
    }
}
