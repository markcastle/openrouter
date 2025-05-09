namespace OpenRouter.Abstractions
{
    /// <summary>
    /// Represents a single choice in a chat completion response.
    /// </summary>
    public class Choice
    {
        /// <summary>
        /// Gets or sets the message associated with this choice.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("message")]
        public Message Message { get; set; } = new Message();
    }
}
