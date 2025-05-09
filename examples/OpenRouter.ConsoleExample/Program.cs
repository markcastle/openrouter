using OpenRouter.Abstractions;
using OpenRouter.Client.Core;
using OpenRouter.Client.Core.Adapters;
using OpenRouter.Client.SystemTextJson;

namespace OpenRouter.ConsoleExample
{
    /// <summary>
    /// Entry point for the OpenRouter console example.
    /// Demonstrates how to configure and use the OpenRouter client.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Main entry point. Configures the client and sends a sample chat completion request.
        /// </summary>
        /// <param name="args">Command-line arguments.</param>
        public static async Task Main(string[] args)
        {
            // Retrieve API key from environment variable for security
            string? apiKey = Environment.GetEnvironmentVariable("OPENROUTER_API_KEY");
            if (string.IsNullOrWhiteSpace(apiKey))
            {
                Console.WriteLine("ERROR: The environment variable 'OPENROUTER_API_KEY' is not set. Please set it to your OpenRouter API key.");
                return;
            }

            // Debug output for API key
            Console.WriteLine($"[DEBUG] API key length: {apiKey.Length}");
            Console.WriteLine($"[DEBUG] API key is null or empty: {string.IsNullOrWhiteSpace(apiKey)}");
            Console.WriteLine($"[DEBUG] Environment variable OPENROUTER_API_KEY: {(apiKey != null ? "SET" : "NOT SET")}");
            if (!string.IsNullOrEmpty(apiKey) && apiKey.Length > 8)
            {
                Console.WriteLine($"[DEBUG] API key preview: {apiKey.Substring(0, 4)}...{apiKey.Substring(apiKey.Length - 4)}");
            }

            if (apiKey != null)
            {
                OpenRouterClientBuilder builder = new OpenRouterClientBuilder()
                    .WithApiKey(apiKey)
                    .WithBaseUrl("https://openrouter.ai/api/v1/")
                    .WithHttpClient(new BasicHttpClientAdapter())
                    .WithSerializer(new SystemTextJsonSerializer())
                    .WithTimeout(30);

                OpenRouterClientOptions options = builder.Build();

                var client = new OpenRouterClient(
                    new BasicHttpClientAdapter(),
                    new SystemTextJsonSerializer(),
                    options
                );

                // TODO: Replace with actual request/response models

                var chatRequest = new ChatCompletionRequest
                {
                    Model = "openrouter/auto",
                    Messages = new List<Message>
                    {
                        new Message
                        {
                            Role = "user",
                            Content = "Hello!, Whats the capital of France?"
                        }
                    }
                };
            
                try
                {
                    ChatCompletionResponse response = await client.SendAsync<ChatCompletionRequest, ChatCompletionResponse>(chatRequest);
                    Console.WriteLine(response.Choices[0].Message.Content);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Request failed: {ex.Message}");
                }
            }
        }
    }

    /// <summary>
    /// Represents a chat completion request for the OpenRouter API.
    /// </summary>
    public class ChatCompletionRequest
    {
        /// <summary>
        /// Gets or sets the model to use (e.g., "gpt-3.5-turbo").
        /// </summary>
        public string Model { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the list of messages in the conversation.
        /// </summary>
        public List<Message> Messages { get; set; } = new List<Message>();
    }

    /// <summary>
    /// Represents a chat completion response from the OpenRouter API.
    /// </summary>
    public class ChatCompletionResponse
    {
        /// <summary>
        /// Gets or sets the list of choices returned by the API.
        /// </summary>
        public List<Choice> Choices { get; set; } = new List<Choice>();
    }

    /// <summary>
    /// Represents a single choice in a chat completion response.
    /// </summary>
    public class Choice
    {
        /// <summary>
        /// Gets or sets the message associated with this choice.
        /// </summary>
        public Message Message { get; set; } = new Message();
    }

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
