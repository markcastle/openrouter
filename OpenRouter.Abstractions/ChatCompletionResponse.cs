using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace OpenRouter.Abstractions
{
    /// <summary>
    /// Represents a chat completion response from the OpenRouter API.
    /// </summary>
    public class ChatCompletionResponse
    {
        /// <summary>
        /// Gets or sets the list of choices returned by the API.
        /// </summary>
        [System.Text.Json.Serialization.JsonPropertyName("choices")]
        public List<Choice> Choices { get; set; } = new List<Choice>();
    }

}
