# PLANNING.md - OpenRouter API Client for .NET Standard 2.1 (Unity Compatible)

## Implementation Guidelines for AI Agent

When implementing this project, the AI agent should:

1. **Always refer back to the TASKS.md document** before starting any implementation task to maintain overall perspective
2. **Follow the EPIC numbering** as a guide for implementation order
3. **Mark tasks as completed** in TASKS.md as they are finished
4. **Maintain test coverage targets** of 100% throughout development
5. **Update documentation** as features are implemented
6. **Consider Unity compatibility** for all design decisions

The AI agent should periodically review progress against the TASKS.md document to ensure that the project stays on track and follows the architectural vision outlined in this document.

## Project Vision

Create a clean, efficient, and robust OpenRouter API client targeting .NET Standard 2.1 specifically for Unity compatibility with 100% code coverage through comprehensive testing.

## Technical Requirements

1. **.NET Standard 2.1 Targeting**:
   - Ensures compatibility with Unity
   - No dependencies that would break Unity compatibility
   - Minimal external dependencies overall

2. **API Coverage**:
   - Authentication
   - Chat completions
   - Models listing and information
   - Provider routing
   - Streaming support
   - Error handling

3. **Testing Strategy**:
   - 100% code coverage with xUnit
   - Unit tests for all components
   - Integration tests with mocked HTTP responses
   - Edge case testing
   - Performance testing
   - Stress testing for streaming

4. **Unity-Specific Considerations**:
   - Thread safety
   - Main thread callbacks
   - Compatible networking approaches
   - Memory management

## Project Structure

The solution will be organized into multiple focused projects:

1. **OpenRouter.Abstractions**
   - Contains interfaces, base models, and contracts
   - No dependencies on concrete implementations
   - Acts as a foundation for all other projects

2. **OpenRouter.Client.Core**
   - Core implementation of the client
   - References only OpenRouter.Abstractions
   - Contains default implementations

3. **OpenRouter.Client.SystemTextJson**
   - System.Text.Json based serialization for .NET 6+
   - Optional, depends on OpenRouter.Abstractions
   - Optimized for newer .NET versions

4. **OpenRouter.Client.NewtonsoftJson**
   - Json.NET based serialization for Unity
   - Optional, depends on OpenRouter.Abstractions
   - Compatible with Unity's JSON requirements

5. **OpenRouter.Client.DependencyInjection**
   - Microsoft DI integration
   - Optional, depends on OpenRouter.Abstractions
   - Extension methods for IServiceCollection

6. **OpenRouter.Client.Resilience**
   - ResilientHttpClient integration
   - Optional, depends on OpenRouter.Abstractions
   - Configurable resilience patterns

Each project will have its own corresponding test project to ensure 100% code coverage:
- OpenRouter.Abstractions.Tests
- OpenRouter.Client.Core.Tests
- OpenRouter.Client.SystemTextJson.Tests
- OpenRouter.Client.NewtonsoftJson.Tests
- OpenRouter.Client.DependencyInjection.Tests
- OpenRouter.Client.Resilience.Tests

## Architecture

### Core Components

1. **Client**:
   - `IOpenRouterClient` - Primary interface for all operations
   - `OpenRouterClient` - Concrete implementation

2. **Configuration**:
   - `OpenRouterClientOptions` - Configuration options
   - `OpenRouterClientBuilder` - Fluent builder for client construction

3. **Authentication**:
   - `AuthenticationService` - Handles API key and bearer token auth

4. **Models**:
   - Request/response models for each API endpoint
   - DTOs for data transfer
   - Serialization/deserialization helpers

5. **HTTP Layer**:
   - `IHttpClientAdapter` - Abstraction over HTTP clients
   - `HttpClientAdapter` - Default implementation using HttpClient
   - `ResilientHttpClientAdapter` - Optional implementation using ResilientHttpClient
   - `IHttpClientFactory` - For testability

6. **Streaming**:
   - `IStreamProcessor` - Processes streaming responses
   - Event-based notification system

7. **Error Handling**:
   - Custom exceptions
   - Error response models
   - Retry policies

### Namespace Structure

```
OpenRouter.Client
├── Authentication
├── Configuration
├── Exceptions
├── Http
├── Models
│   ├── Requests
│   ├── Responses
│   └── Common
├── Services
│   ├── Chat
│   ├── Models
│   └── Providers
├── Streaming
└── Utils
```

## API Design

### Client Interface

```csharp
public interface IOpenRouterClient
{
    // Core APIs
    Task<ChatCompletionResponse> CreateChatCompletionAsync(
        ChatCompletionRequest request,
        CancellationToken cancellationToken = default);
    
    Task<List<Model>> ListModelsAsync(
        CancellationToken cancellationToken = default);
    
    // Streaming
    IAsyncEnumerable<ChatCompletionChunk> StreamChatCompletionAsync(
        ChatCompletionRequest request,
        CancellationToken cancellationToken = default);
    
    // Events
    event EventHandler<StreamProgressEventArgs> OnStreamProgress;
    event EventHandler<ErrorEventArgs> OnError;
}
```

### Request/Response Models

Core models will mirror the OpenRouter API schema but with C# idiomatic naming and structure:

```csharp
public class ChatCompletionRequest
{
    public string Model { get; set; }
    public List<Message> Messages { get; set; }
    public float? Temperature { get; set; }
    public int? MaxTokens { get; set; }
    public bool? Stream { get; set; }
    public ProviderPreference Provider { get; set; }
    // Additional parameters...
}

public class ChatCompletionResponse
{
    public string Id { get; set; }
    public string Object { get; set; }
    public long Created { get; set; }
    public string Model { get; set; }
    public List<Choice> Choices { get; set; }
    public UsageInfo Usage { get; set; }
}
```

## Provider-Agnostic JSON Serialization

The client will use a provider-agnostic approach to JSON serialization and deserialization:

1. **Abstraction Layer**:
   ```csharp
   public interface IJsonSerializer
   {
       string Serialize<T>(T value);
       T Deserialize<T>(string json);
       T Deserialize<T>(Stream json);
       ValueTask<T> DeserializeAsync<T>(Stream json, CancellationToken cancellationToken = default);
   }
   ```

2. **Implementations**:
   - `SystemTextJsonSerializer`: Implementation using System.Text.Json for .NET 6+
   - `NewtonsoftJsonSerializer`: Implementation using Newtonsoft.Json for Unity compatibility

3. **Factory and Registration**:
   ```csharp
   // Factory interface
   public interface IJsonSerializerFactory
   {
       IJsonSerializer CreateSerializer();
   }
   
   // Registration in client options
   public class OpenRouterClientOptions
   {
       public IJsonSerializer JsonSerializer { get; set; }
       // Other options...
   }
   ```

4. **Default Behavior**:
   - Auto-detect the best serializer based on runtime environment
   - Fallback to available serializer if preferred is not present
   - Allow explicit configuration via options

5. **Performance Optimizations**:
   - Caching of serialization metadata
   - Reuse of serializer instances
   - Memory-efficient deserialization

## Data Flow

1. **Request Creation**:
   - Create request object
   - Validate request parameters
   - Apply default values if needed

2. **API Interaction**:
   - Serialize request to JSON
   - Apply authentication
   - Send HTTP request
   - Handle HTTP-level errors

3. **Response Processing**:
   - Deserialize response
   - Map to appropriate model
   - Handle API-level errors

4. **Streaming Responses**:
   - Parse SSE format
   - Chunk and process stream
   - Emit events for each chunk
   - Handle stream completion and errors

## Microsoft Dependency Injection Integration

The client will support Microsoft's dependency injection container via an optional project:

1. **Service Registration**:
   ```csharp
   public static class OpenRouterServiceCollectionExtensions
   {
       public static IServiceCollection AddOpenRouter(
           this IServiceCollection services, 
           Action<OpenRouterClientOptions> configureOptions = null)
       {
           // Register services
           return services;
       }
       
       public static IServiceCollection AddOpenRouterClient(
           this IServiceCollection services,
           Action<OpenRouterClientOptions> configureOptions = null)
       {
           // Register client and dependencies
           return services;
       }
   }
   ```

2. **Lifetime Management**:
   - Register core client as singleton by default
   - Allow configuration of lifetimes via options
   - Proper disposal of resources

3. **Options Pattern Integration**:
   - Support for IOptions<OpenRouterClientOptions>
   - Support for named clients with different configurations
   - Easy configuration via appsettings.json

4. **Factory Registration**:
   ```csharp
   services.AddSingleton<IOpenRouterClientFactory, OpenRouterClientFactory>();
   ```

5. **Named Clients**:
   ```csharp
   // Register multiple clients with different configurations
   services.AddOpenRouterClient("client1", options => { /* configure */ });
   services.AddOpenRouterClient("client2", options => { /* configure */ });
   
   // Resolve using factory
   var client = factory.CreateClient("client1");
   ```

This approach ensures proper integration with any application using Microsoft's DI container while keeping it optional for environments like Unity that may use different dependency injection approaches.

## Error Handling Strategy

1. **Exception Types**:
   - `OpenRouterException` - Base exception
   - `AuthenticationException` - Auth failures
   - `RateLimitException` - Rate limiting
   - `InvalidRequestException` - Bad requests
   - `ServiceException` - Server-side errors

2. **Error Information**:
   - Status code
   - Error message
   - Request ID for debugging
   - Detailed information where available

3. **Retry Policies**:
   - Automatic retries for transient failures
   - Configurable retry limits
   - Exponential backoff

## Optional ResilientHttpClient Integration

The OpenRouter API client will support optional integration with ResilientHttpClient, a drop-in replacement for HttpClient that adds resiliency patterns:

1. **Adapter Pattern**:
   - Create a specific adapter for ResilientHttpClient
   - Maintain the same interface as the standard HttpClient adapter
   - Allow configuration through options

2. **Configuration Options**:
   - `UseResilientHttpClient` flag to enable/disable the resilient client
   - `ResilientHttpClientOptions` for configuring resilience parameters
   - Factory method to create the appropriate client based on configuration

3. **Default Settings**:
   - Circuit breaker with appropriate defaults for API clients
   - Retry policies optimized for external API calls
   - Timeout handling specific to OpenRouter API

4. **Benefits**:
   - Improved reliability for unstable network conditions
   - Automatic handling of transient failures
   - Better performance under high load
   - Protection against cascading failures

5. **Implementation Approach**:
   - Keep ResilientHttpClient as an optional dependency
   - Allow the client to function without it
   - Provide clear documentation on how to enable and configure it

## Performance Considerations

1. **Memory Efficiency**:
   - Minimize allocations
   - Use ArrayPool for buffers
   - Streaming for large responses

2. **JSON Serialization**:
   - System.Text.Json for performance
   - Source generators for static JSON serialization

3. **HTTP Optimization**:
   - Connection pooling
   - HTTP/2 support where available
   - Compression

4. **Unity-Specific**:
   - Thread synchronization
   - Avoid blocking operations
   - Main thread callbacks

## Testing Strategy

1. **Unit Testing**:
   - Test each component in isolation
   - Mock dependencies
   - Test failure modes
   - Parameterized tests for edge cases

2. **Integration Testing**:
   - Test client against mock server
   - Verify correct request/response handling
   - Test authentication flow

3. **Special Test Cases**:
   - Unicode and special character handling
   - Very large responses
   - Very slow responses
   - Connection drops
   - Rate limiting

4. **Performance Testing**:
   - Benchmark core operations
   - Memory usage tests
   - Streaming performance

## Unity Compatibility

1. **Threading Model**:
   - Respect Unity's main thread requirements
   - Safe cross-thread operations

2. **Memory Management**:
   - Avoid GC pressure
   - Proper disposal of resources

3. **Platform Considerations**:
   - Mobile network handling
   - WebGL limitations
   - Platform-specific optimizations

## Documentation Strategy

1. **XML Comments**:
   - All public APIs fully documented
   - Example usage in comments

2. **README and Wiki**:
   - Quick start guide
   - Advanced usage examples
   - Best practices

3. **Sample Projects**:
   - Basic console application
   - Unity integration example

## Release and Distribution

1. **NuGet Package**:
   - Clear versioning
   - Proper dependencies
   - Symbol packages for debugging

2. **Unity Package**:
   - Unity Package Manager compatible
   - Sample scenes
   - Documentation



RULES:

NEver edit Unit tests in order to get them to pass. Unless the tests are verifiably bad.