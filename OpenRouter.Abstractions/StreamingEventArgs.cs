namespace OpenRouter.Abstractions
{
    /// <summary>
    /// Event arguments for streaming updates from the API.
    /// </summary>
    public class StreamingEventArgs : OpenRouterEventArgs
    {
        /// <summary>
        /// Gets or sets the chunk of streamed data.
        /// </summary>
        public string? DataChunk { get; set; }
    }
}
