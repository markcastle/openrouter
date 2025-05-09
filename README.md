# 🚀 OpenRouter API Client (.NET Standard 2.1 / Unity Compatible)

[![Build Status](https://github.com/markcastle/openrouter/actions/workflows/ci.yml/badge.svg?branch=master)](https://github.com/markcastle/openrouter/actions)
[![Coverage](https://img.shields.io/badge/coverage-87.4%25-brightgreen.svg)](https://github.com/markcastle/openrouter/actions) <!-- Update the badge URL percentage when coverage changes -->
[![NuGet](https://img.shields.io/nuget/v/OpenRouter.Client.Core.svg)](https://www.nuget.org/packages/OpenRouter.Client.Core)

> **Note:** NuGet packages have not yet been published. They will be released once the software is fully tested and we feel it is ready for production use.

---

> **Disclaimer:**
> 
> This repo is in **active development** and is being *"vibe coded"*. Use it at your own risk.
> Our goal is to build a stable, well-tested, and optimized library. Once it is fully tested and optimized, we hope to release it as a stable package.

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

## 📅 Project Roadmap

**Setup & DevOps**
- [x] Solution and project setup (Core, Abstractions, SystemTextJson, NewtonsoftJson, DI, Resilience)
- [x] Test projects and CI/CD pipeline with coverage
- [x] Static code analysis (StyleCop)
- [x] PR/issue templates & changelog
- [ ] Automated package versioning & NuGet publishing
- [ ] Release documentation & wiki structure

**Core Architecture & Serialization**
- [x] Serializer abstraction & SOLID DI
- [x] Exception hierarchy and error handling
- [x] Core-only abstraction (no concrete serializer dependency)
- [x] Remove all serializer attributes from models (pure POCOs)
- [x] Custom property naming/mapping for OpenRouter contract
- [ ] Custom naming policy support (System.Text.Json/Newtonsoft)
- [ ] Performance optimizations (Newtonsoft)
- [ ] Configuration options & extension methods for easy setup

**HTTP Client & Infrastructure**
- [x] HTTP adapter/factory/interfaces
- [x] Request/response/parameter models
- [x] Basic CRUD operations (GET/POST/PUT/DELETE)
- [x] Streaming support (infrastructure)
- [ ] Async initialization, event handlers, logging integration
- [ ] Client-side timeout handling

**Client Builder & Configuration**
- [x] Fluent builder for configuration
- [x] Client-side validation logic
- [ ] Environment handling (dev/prod), versioning support

**API Endpoints**
- [ ] Chat completions endpoint (basic)
- [ ] Parameter support (temperature, max tokens, etc.)
- [ ] Model listing/details, provider routing, cost estimation
- [ ] Token counting utilities, response validation, error handling

**Streaming & Events**
- [x] Event-based notification architecture
- [ ] Streaming infrastructure & SSE parsing
- [ ] Event-based notification for streaming, buffer management

**Dependency Injection & Integration**
- [ ] MS.Extensions.DependencyInjection integration & options pattern
- [ ] Named clients, scoped disposal, DI container tests
- [ ] DI integration documentation

**Resilience & Advanced HTTP**
- [ ] ResilientHttpClient integration (retry/circuit breaker)
- [ ] Resilience event handling, metrics, and tests

**Unity-Specific**
- [ ] Unity thread dispatcher, main thread callbacks, editor integration
- [ ] Unity networking/memory/error/platform optimizations

**Testing & Coverage**
- [x] 87.4%+ code coverage with xUnit
- [ ] 100% code coverage (add tests for uncovered code, edge/failure cases)
- [ ] Advanced test infrastructure: mocks, fixtures, performance, edge cases

**Examples & Documentation**
- [x] Example console app and usage docs
- [ ] More usage examples (including streaming)
- [ ] Improve API samples and documentation
- [ ] Wiki and advanced guides

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
- **Targetting: 100% xUnit Coverage:** All builder and config logic are / or will be fully tested, including edge/failure cases

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

> **Note:** NuGet packages have not yet been published. They will be released once the software is fully tested and we feel it is ready for production use.

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
# Set your actual OpenRouter API key as an environment variable
# (Windows CMD)
set OPENROUTER_API_KEY=sk-...yourkey...
# (PowerShell)
$env:OPENROUTER_API_KEY="sk-...yourkey..."
# (Linux/macOS)
export OPENROUTER_API_KEY=sk-...yourkey...

dotnet run
```

**Expected output:**

```
[DEBUG] API key length: 73
[DEBUG] API key is null or empty: False
[DEBUG] Environment variable OPENROUTER_API_KEY: SET
[DEBUG] API key preview: sk-o...9509
The capital of France is Paris. It is also the largest city in France and one of the most populous cities in Europe. Paris is known for its iconic landmarks such as the Eiffel Tower, the Louvre Museum, Notre-Dame Cathedral, and the Arc de Triomphe.
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
   // Now ISerializer will resolve to SystemTextJsonSerializer (advanced) or SystemTextJsonAdapter (basic, manual usage)
   ```

3. **Manual instantiation (basic):**
   ```csharp
   using OpenRouter.Client.SystemTextJson;
   ISerializer serializer = new SystemTextJsonAdapter();
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
- Full serialization/deserialization test coverage for both System.Text.Json and Newtonsoft.Json adapters, including:
  - snake_case contract compliance
  - expected, edge, and failure cases
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
- Moved SystemTextJsonSerializer (basic adapter) out of Core and into SystemTextJson project
- All models are now pure POCOs (no serializer attributes)
- Serialization configuration (snake_case, naming, converters) is handled externally for both System.Text.Json and Newtonsoft.Json
- Full serialization/deserialization test coverage for both adapters (snake_case, expected, edge, failure cases)
- Completed: Chat completions endpoint implementation, serialization, error handling, and full test coverage

---

## License

MIT © Captive Reality Ltd 2025
