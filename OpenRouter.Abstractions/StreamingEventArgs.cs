using System;

namespace OpenRouter.Abstractions
{
    /// <summary>
    /// Event arguments for streaming updates from the API.
    /// </summary>
    public class StreamingEventArgs : OpenRouterEventArgs
    {
        /// <summary>
        /// Gets the data received in the streaming update.
        /// </summary>
        public object Data { get; }


        /// <summary>
        /// Initializes a new instance of <see cref="StreamingEventArgs"/>.
        /// </summary>
        /// <param name="data">The streaming data.</param>
        public StreamingEventArgs(object data)
        {
            Data = data;
        }
    }
}
