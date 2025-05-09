namespace OpenRouter.Abstractions
{
    /// <summary>
    /// Represents a single message in a chat conversation.
    /// </summary>
    public class Message
    {
        /// <summary>
        /// Gets or sets the role of the message sender (e.g., "user", "assistant").
        /// </summary>
        public string Role { get; set; } = "user";

        /// <summary>
        /// Gets or sets the content of the message.
        /// </summary>
        public string Content { get; set; } = string.Empty;
    }
}
