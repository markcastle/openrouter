using System.Collections.Generic;

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
        public string Model { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the list of messages in the conversation.
        /// </summary>
        public List<Message> Messages { get; set; } = new List<Message>();

        /// <summary>
        /// Gets or sets the temperature for sampling (optional).
        /// </summary>
        public float? Temperature { get; set; }

        /// <summary>
        /// Gets or sets the maximum number of tokens to generate (optional).
        /// </summary>
        public int? MaxTokens { get; set; }
    }

}
