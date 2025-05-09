# 🖥️ OpenRouter Console Example

This example demonstrates how to configure and use the OpenRouter client in a simple .NET console application.

---

## Example Usage

```csharp
using OpenRouter.Client.Core;
using OpenRouter.Client.Core.Adapters;
using OpenRouter.Abstractions;

// Build options
var builder = new OpenRouterClientBuilder()
    .WithApiKey("your-api-key")
    .WithBaseUrl("https://openrouter.ai/api/v1/")
    .WithHttpClient(new BasicHttpClientAdapter())
    .WithSerializer(new SystemTextJsonSerializer())
    .WithTimeout(30);

var options = builder.Build();

// Create client
var client = new OpenRouterClient(
    new BasicHttpClientAdapter(),
    new SystemTextJsonSerializer(),
    options
);

// Build your request (replace with your actual request model)
var chatRequest = new ChatCompletionRequest
{
    Model = "gpt-3.5-turbo",
    Messages = new List<Message> { new Message { Role = "user", Content = "Hello!" } }
};

var response = await client.SendAsync<ChatCompletionRequest, ChatCompletionResponse>(chatRequest);
Console.WriteLine(response.Choices[0].Message.Content);
```

---

- Replace `your-api-key` with your OpenRouter API key.
- Implement or reference the required models (`ChatCompletionRequest`, `ChatCompletionResponse`, `Message`).
- This example assumes all referenced classes and adapters are available in your solution.
