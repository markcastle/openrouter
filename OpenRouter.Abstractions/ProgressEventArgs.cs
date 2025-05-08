namespace OpenRouter.Abstractions
{
    /// <summary>
    /// Event arguments for reporting progress of a long-running operation.
    /// </summary>
    public class ProgressEventArgs : OpenRouterEventArgs
    {
        /// <summary>
        /// Gets or sets the percentage of completion.
        /// </summary>
        public double Percentage { get; set; }

        /// <summary>
        /// Gets or sets an optional status message.
        /// </summary>
        public string? Status { get; set; }
    }
}
