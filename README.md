# 🚀 OpenRouter API Client (.NET Standard 2.1 / Unity Compatible)

[![Build Status](https://github.com/markcastle/openrouter/actions/workflows/ci.yml/badge.svg?branch=master)](https://github.com/markcastle/openrouter/actions)
[![Coverage](https://img.shields.io/badge/coverage-100%25-brightgreen.svg)](https://github.com/markcastle/openrouter/actions) <!-- Update the badge URL percentage when coverage changes -->
[![NuGet](https://img.shields.io/nuget/v/OpenRouter.Client.Core.svg)](https://www.nuget.org/packages/OpenRouter.Client.Core)

---

## 🎯 Project Vision

A robust, modular, and developer-friendly OpenRouter API client targeting .NET Standard 2.1 for maximum compatibility—including Unity—with 100% xUnit test coverage, extensibility, and advanced DI and HTTP resilience support.

---

## ✨ Features
- 🧩 **Multi-project architecture** for clean separation of concerns
- 🎮 **Unity compatible** (.NET Standard 2.1, no Unity-breaking dependencies)
- 🔄 **Provider-agnostic JSON serialization** (System.Text.Json & Newtonsoft.Json)
- 🏗️ **Optional Microsoft DI and ResilientHttpClient**
- 🧠 **SOLID, KISS, YAGNI** principles
- 📚 **Comprehensive documentation and samples**
- 🌊 **Streaming, error handling, and resilience**

---

## 🏗️ Solution Structure

- `OpenRouter.Abstractions` — Interfaces, base models, contracts (no concrete dependencies)
- `OpenRouter.Client.Core` — Core implementation, default logic
- `OpenRouter.Client.SystemTextJson` — System.Text.Json serialization for .NET 6+
- `OpenRouter.Client.NewtonsoftJson` — Newtonsoft.Json serialization for Unity
- `OpenRouter.Client.DependencyInjection` — Microsoft DI integration (optional)
- `OpenRouter.Client.Resilience` — ResilientHttpClient integration (optional)
- Each has a corresponding `.Tests` project (xUnit)

---

## 🧠 Architecture Overview

- **Client:** `IOpenRouterClient` interface (implementation coming soon)
- **Configuration:** `OpenRouterClientOptions` with nested option objects and fluent builder for safe, flexible setup
- **Validation:** All options are fully validated for required fields and sensible defaults
- **Authentication:** API key and bearer token support
- **Models:** Request/response models mirror the OpenRouter API schema
- **HTTP Layer:** Adapter pattern for HTTP clients and resilience
- **Event Notification:** Extensible event system for streaming and errors
- **Error Handling:** Custom exception hierarchy for robust error reporting
- **SOLID Compliance:** No factories or tight coupling; everything is interface-driven and easily testable
- **100% xUnit Coverage:** All builder and config logic is fully tested, including edge/failure cases

---

## 🚀 Getting Started

1. **Clone the repository**
   ```sh
   git clone https://github.com/markcastle/openrouter.git
   cd openrouter
   ```
2. **Restore dependencies**
   ```sh
   dotnet restore
   ```
3. **Run all tests (optional, recommended)**
   ```sh
   dotnet test
   ```
4. **Try the example console app**
   See [Usage Example](#usage-example) above.

---

## 📦 Installation

1. **Via NuGet**
   - Core: `dotnet add package OpenRouter.Client.Core`
   - Serialization: `dotnet add package OpenRouter.Client.SystemTextJson` or `OpenRouter.Client.NewtonsoftJson`
   - DI/Resilience (optional): `dotnet add package OpenRouter.Client.DependencyInjection` / `OpenRouter.Client.Resilience`
2. **Unity**
   - Use `.NET Standard 2.1` compatible DLLs and `OpenRouter.Client.NewtonsoftJson`.

---

## 💡 Usage Example

### 🖥️ Chat Completions Endpoint Example

Here's how to call the `/chat/completions` endpoint using the strongly-typed client:

```csharp
using OpenRouter.Client.Core;
using OpenRouter.Abstractions;

var options = new OpenRouterClientOptions
{
    Http = new HttpOptions { BaseUrl = "https://openrouter.ai/api/v1/" },
    Authentication = new AuthenticationOptions { ApiKey = "your-api-key" }
};
var client = new OpenRouterClient(
    new YourHttpClientAdapter(), // Implement IHttpClientAdapter as needed
    new YourSerializer(),        // Implement ISerializer as needed
    options
);

var request = new ChatCompletionRequest
{
    Model = "gpt-3.5-turbo",
    Messages = new List<Message>
    {
        new Message { Role = "user", Content = "Hello, who are you?" }
    }
};

try
{
    ChatCompletionResponse response = await client.CreateChatCompletionAsync(request);
    string reply = response.Choices.First().Message.Content;
    Console.WriteLine($"Assistant: {reply}");
}
catch (System.Net.Http.HttpRequestException ex)
{
    // Handle API errors (e.g., invalid API key, bad request)
    Console.WriteLine($"API error: {ex.Message}");
}
```

- All request/response models are strongly typed and match the OpenRouter API spec.
- API errors throw `HttpRequestException` with details.
- See the `OpenRouterClientChatCompletionsTests` for more usage patterns and edge cases.

### 🖥️ Console Example Project

A complete, runnable example is provided in [`/examples/OpenRouter.ConsoleExample`](./examples/OpenRouter.ConsoleExample/).

**To run the example:**

```sh
cd examples/OpenRouter.ConsoleExample
# Edit Program.cs and set your actual OpenRouter API key
# Optionally, update the request model to your needs

dotnet run
```

This demonstrates how to configure and use the OpenRouter client with real API calls, including error handling and placeholder models.

---

### Using the Fluent Builder (Recommended)

```csharp
var builder = new OpenRouterClientBuilder()
    .WithApiKey("your-api-key")
    .WithBaseUrl("https://openrouter.ai/api/v1/")
    .WithHttpClient(yourHttpClientAdapter) // Implement IHttpClientAdapter as needed
    .WithSerializer(yourSerializer)        // Implement ISerializer as needed
    .WithTimeout(30);                     // Timeout in seconds (optional)

var options = builder.Build();
// Pass options to your client implementation when ready
```

### Manual Options Construction (Advanced)

```csharp
var options = new OpenRouterClientOptions
{
    Authentication = new AuthenticationOptions { ApiKey = "your-api-key" },
    Http = new HttpOptions { BaseUrl = "https://openrouter.ai/api/v1/", TimeoutSeconds = 30 }
};
```

### Validating Options

```csharp
options.Validate(); // Throws if any required config is missing or invalid
```

---

---

## ⚙️ Serializer Configuration (DI & Unity/Manual)

### 🖥️ .NET Core / ASP.NET (System.Text.Json via DI)

1. **Install:**
   ```sh
   dotnet add package OpenRouter.Client.SystemTextJson
   ```
2. **Register in DI:**
   ```csharp
   using OpenRouter.Client.SystemTextJson;
   // ...
   services.AddSystemTextJsonSerializer();
   // Now ISerializer will resolve to SystemTextJsonSerializer
   ```
3. **Inject or resolve ISerializer as needed.**

### 🎮 Unity or Manual Instantiation (Newtonsoft.Json)

1. **Install:**
   - Add `OpenRouter.Client.NewtonsoftJson` DLL or NuGet (no MS DI dependency, Unity safe)
2. **Manual Usage:**
   ```csharp
   using OpenRouter.Client.NewtonsoftJson;
   using OpenRouter.Abstractions;
   
   // Register custom converters (e.g., DateTimeOffsetConverter)
   var converters = new List<Newtonsoft.Json.JsonConverter>
   {
       new OpenRouter.Client.NewtonsoftJson.DateTimeOffsetConverter()
   };
   ISerializer serializer = new NewtonsoftJsonSerializer(converters);
   var client = new OpenRouterClient(
       new OpenRouterClientOptions { /* ... */ },
       serializer // pass as dependency
   );
   ```
   You can also pass a custom JsonSerializerSettings if you need more control.
3. **No DI required.**

#### 🤔 Which Should I Use?
- **.NET Core/ASP.NET:** Prefer System.Text.Json with DI for best performance and integration.
- **Unity or platforms without MS DI:** Use NewtonsoftJson and instantiate manually.

---

## 🧩 Extensibility & Unity Compatibility
- **Interfaces-first:** All core logic is interface-driven for easy extension and testing.
- **Unity:** Designed for main-thread safety, minimal allocations, and compatible serialization.
- **DI/Resilience:** Optional integration with Microsoft.Extensions.DependencyInjection and ResilientHttpClient.

---

## 🧪 Testing
- 100% xUnit code coverage target
- Unit tests for all components and edge cases
- Integration tests with mocked HTTP
- Special test cases for streaming, Unicode, and error handling

---

## 🤝 Contributing

1. Fork and clone the repo
2. Create a feature branch (`git checkout -b feature/your-feature`)
3. Add or update tests for any new features or bug fixes
4. Run all tests and ensure code coverage
5. Submit a pull request with a clear description

---

## Changelog

**2025-05-09**
- Completed: Chat completions endpoint implementation, serialization, error handling, and full test coverage

---

## License

MIT © Captive Reality Ltd 2025
