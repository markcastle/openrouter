namespace OpenRouter.Abstractions
{
    /// <summary>
    /// Event arguments for a request being sent to the API.
    /// </summary>
    public class RequestEventArgs : OpenRouterEventArgs
    {
        /// <summary>
        /// Gets or sets the request options associated with the event.
        /// </summary>
        public HttpRequestOptions? RequestOptions { get; set; }
    }
}
