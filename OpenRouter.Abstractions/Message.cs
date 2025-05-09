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
        [System.Text.Json.Serialization.JsonPropertyName("role")]
        public string Role { get; set; } = "user";

        /// <summary>
        /// Gets or sets the content of the message.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("content")]
        public string Content { get; set; } = string.Empty;
    }
}
