using System;

namespace OpenRouter.Abstractions
{
    /// <summary>
    /// Base class for all OpenRouter event argument types.
    /// </summary>
    public abstract class OpenRouterEventArgs : EventArgs
    {
        /// <summary>
        /// Gets the UTC timestamp when the event occurred.
        /// </summary>
        public DateTime TimestampUtc { get; }

        /// <summary>
        /// Initializes a new instance of <see cref="OpenRouterEventArgs"/>.
        /// </summary>
        protected OpenRouterEventArgs()
        {
            TimestampUtc = DateTime.UtcNow;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="OpenRouterEventArgs"/> with a custom timestamp.
        /// </summary>
        /// <param name="timestampUtc">The UTC timestamp for the event.</param>
        protected OpenRouterEventArgs(DateTime timestampUtc)
        {
            TimestampUtc = timestampUtc;
        }
    }
}
