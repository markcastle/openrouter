# OpenRouter API Client (.NET Standard 2.1 / Unity Compatible)

[![Build Status](https://github.com/markcastle/openrouter/actions/workflows/ci.yml/badge.svg?branch=master)](https://github.com/markcastle/openrouter/actions)
[![Coverage](https://img.shields.io/badge/coverage-100%25-brightgreen.svg)](https://github.com/markcastle/openrouter/actions) <!-- Update the badge URL percentage when coverage changes -->
[![NuGet](https://img.shields.io/nuget/v/OpenRouter.Client.Core.svg)](https://www.nuget.org/packages/OpenRouter.Client.Core)

---

## Project Vision

A robust, modular, and developer-friendly OpenRouter API client targeting .NET Standard 2.1 for maximum compatibility—including Unity—with 100% xUnit test coverage, extensibility, and advanced DI and HTTP resilience support.

---

## Features
- **Multi-project architecture** for clean separation of concerns
- **Unity compatible** (.NET Standard 2.1, no Unity-breaking dependencies)
- **Provider-agnostic JSON serialization** (System.Text.Json & Newtonsoft.Json)
- **Optional Microsoft DI and ResilientHttpClient**
- **SOLID, KISS, YAGNI** principles
- **Comprehensive documentation and samples**
- **Streaming, error handling, and resilience**

---

## Solution Structure

- `OpenRouter.Abstractions` — Interfaces, base models, contracts (no concrete dependencies)
- `OpenRouter.Client.Core` — Core implementation, default logic
- `OpenRouter.Client.SystemTextJson` — System.Text.Json serialization for .NET 6+
- `OpenRouter.Client.NewtonsoftJson` — Newtonsoft.Json serialization for Unity
- `OpenRouter.Client.DependencyInjection` — Microsoft DI integration (optional)
- `OpenRouter.Client.Resilience` — ResilientHttpClient integration (optional)
- Each has a corresponding `.Tests` project (xUnit)

---

## Architecture Overview

- **Client:** `IOpenRouterClient` interface and `OpenRouterClient` implementation
- **Configuration:** `OpenRouterClientOptions` and builder pattern
- **Authentication:** API key and bearer token support
- **Models:** Request/response models mirror the OpenRouter API schema
- **HTTP Layer:** Adapter pattern for HTTP clients and resilience
- **Event Notification:** Extensible event system for streaming and errors
- **Error Handling:** Custom exception hierarchy for robust error reporting

---

## Installation

1. **Via NuGet**
   - Core: `dotnet add package OpenRouter.Client.Core`
   - Serialization: `dotnet add package OpenRouter.Client.SystemTextJson` or `OpenRouter.Client.NewtonsoftJson`
   - DI/Resilience (optional): `dotnet add package OpenRouter.Client.DependencyInjection` / `OpenRouter.Client.Resilience`
2. **Unity**
   - Use `.NET Standard 2.1` compatible DLLs and `OpenRouter.Client.NewtonsoftJson`.

---

## Usage Example

```csharp
var client = new OpenRouterClient(new OpenRouterClientOptions
{
    ApiKey = "your-api-key",
    BaseUrl = "https://openrouter.ai/api/v1/"
});

var chatRequest = new ChatCompletionRequest
{
    Model = "gpt-3.5-turbo",
    Messages = new List<Message> { new Message { Role = "user", Content = "Hello!" } }
};

var response = await client.SendAsync<ChatCompletionRequest, ChatCompletionResponse>(chatRequest);
Console.WriteLine(response.Choices[0].Message.Content);
```

---

## Extensibility & Unity Compatibility
- **Interfaces-first:** All core logic is interface-driven for easy extension and testing.
- **Unity:** Designed for main-thread safety, minimal allocations, and compatible serialization.
- **DI/Resilience:** Optional integration with Microsoft.Extensions.DependencyInjection and ResilientHttpClient.

---

## Testing
- 100% xUnit code coverage target
- Unit tests for all components and edge cases
- Integration tests with mocked HTTP
- Special test cases for streaming, Unicode, and error handling

---

## Contributing

1. Fork and clone the repo
2. Create a feature branch (`git checkout -b feature/your-feature`)
3. Add or update tests for any new features or bug fixes
4. Run all tests and ensure code coverage
5. Submit a pull request with a clear description

---

## License

MIT © Captive Reality Ltd 2025
