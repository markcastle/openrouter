using System;

namespace OpenRouter.Abstractions
{
    /// <summary>
    /// Event arguments for reporting progress of a long-running operation.
    /// </summary>
    public class ProgressEventArgs : OpenRouterEventArgs
    {
        /// <summary>
        /// Gets the percentage of progress (0-100).
        /// </summary>
        public int ProgressPercentage { get; }

        /// <summary>
        /// Gets an optional status message.
        /// </summary>
        public string? StatusMessage { get; }


        /// <summary>
        /// Initializes a new instance of <see cref="ProgressEventArgs"/>.
        /// </summary>
        /// <param name="progressPercentage">The progress percentage (0-100).</param>
        /// <param name="statusMessage">An optional status message.</param>
        public ProgressEventArgs(int progressPercentage, string? statusMessage = null)
        {
            ProgressPercentage = progressPercentage;
            StatusMessage = statusMessage;
        }
    }
}
