namespace OpenRouter.Abstractions
{
    /// <summary>
    /// Event arguments for a response received from the API.
    /// </summary>
    public class ResponseEventArgs : OpenRouterEventArgs
    {
        /// <summary>
        /// Gets or sets the response wrapper associated with the event.
        /// </summary>
        public HttpResponseWrapper? Response { get; set; }
    }
}
