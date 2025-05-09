using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace OpenRouter.Abstractions
{
    /// <summary>
    /// Represents a chat completion request for the OpenRouter API.
    /// </summary>
    public class ChatCompletionRequest
    {
        /// <summary>
        /// Gets or sets the model to use for completion (e.g., "gpt-3.5-turbo").
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("model")]
        public string Model { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the list of messages in the conversation.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("messages")]
        public List<Message> Messages { get; set; } = new List<Message>();

        /// <summary>
        /// Gets or sets the temperature for sampling (optional).
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("temperature")]
        public float? Temperature { get; set; }

        /// <summary>
        /// Gets or sets the maximum number of tokens to generate (optional).
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("max_tokens")]
        public int? MaxTokens { get; set; }
    }

}
