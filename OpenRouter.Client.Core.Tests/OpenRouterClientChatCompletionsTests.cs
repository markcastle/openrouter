using Moq;
using OpenRouter.Abstractions;
using OpenRouter.Client.SystemTextJson;

namespace OpenRouter.Client.Core.Tests
{
    /// <summary>
    /// Unit tests for chat completions endpoint in OpenRouterClient.
    /// </summary>
    public class OpenRouterClientChatCompletionsTests
    {
        [Fact]
        public async Task CreateChatCompletionAsync_ReturnsExpectedResponse_OnValidRequest()
        {
            // Arrange
            var expectedContent = "{\"choices\":[{\"message\":{\"role\":\"assistant\",\"content\":\"Hi there!\"}}]}";
            var mockHttpAdapter = new Mock<IHttpClientAdapter>();
            mockHttpAdapter.Setup(a => a.SendAsync(It.IsAny<Abstractions.HttpRequestOptions>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new HttpResponseWrapper { StatusCode = 200, Content = expectedContent, ContentType = "application/json" });

            var serializer = new SystemTextJsonSerializer();
            var options = new OpenRouterClientOptions { Authentication = new AuthenticationOptions { ApiKey = "test" }, Http = new HttpOptions { BaseUrl = "https://openrouter.ai/api/v1/" } };
            var client = new OpenRouterClient(mockHttpAdapter.Object, serializer, options);

            var request = new ChatCompletionRequest
            {
                Model = "gpt-3.5-turbo",
                Messages = new List<Message> { new Message { Role = "user", Content = "Hello!" } }
            };

            // Act
            var response = await client.CreateChatCompletionAsync(request);

            // Assert
            Assert.NotNull(response);
            Assert.NotEmpty(response.Choices);
            Assert.Equal("assistant", response.Choices[0].Message.Role);
            Assert.Equal("Hi there!", response.Choices[0].Message.Content);
        }

        [Fact]
        public async Task CreateChatCompletionAsync_Throws_OnApiError()
        {
            // Arrange
            var mockHttpAdapter = new Mock<IHttpClientAdapter>();
            mockHttpAdapter.Setup(a => a.SendAsync(It.IsAny<Abstractions.HttpRequestOptions>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new HttpResponseWrapper { StatusCode = 400, Content = "{\"error\":\"Bad Request\"}", ContentType = "application/json" });

            var serializer = new SystemTextJsonSerializer();
            var options = new OpenRouterClientOptions { Authentication = new AuthenticationOptions { ApiKey = "test" }, Http = new HttpOptions { BaseUrl = "https://openrouter.ai/api/v1/" } };
            var client = new OpenRouterClient(mockHttpAdapter.Object, serializer, options);

            var request = new ChatCompletionRequest { Model = "gpt-3.5-turbo", Messages = new List<Message>() };

            // Act & Assert
            await Assert.ThrowsAsync<HttpRequestException>(async () => await client.CreateChatCompletionAsync(request));
        }

        [Fact]
        public async Task CreateChatCompletionAsync_ReturnsEmptyChoices_OnEmptyResponse()
        {
            // Arrange
            var mockHttpAdapter = new Mock<IHttpClientAdapter>();
            mockHttpAdapter.Setup(a => a.SendAsync(It.IsAny<Abstractions.HttpRequestOptions>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new HttpResponseWrapper { StatusCode = 200, Content = "{}", ContentType = "application/json" });

            var serializer = new SystemTextJsonSerializer();
            var options = new OpenRouterClientOptions { Authentication = new AuthenticationOptions { ApiKey = "test" }, Http = new HttpOptions { BaseUrl = "https://openrouter.ai/api/v1/" } };
            var client = new OpenRouterClient(mockHttpAdapter.Object, serializer, options);

            var request = new ChatCompletionRequest { Model = "gpt-3.5-turbo", Messages = new List<Message>() };

            // Act
            var response = await client.CreateChatCompletionAsync(request);

            // Assert
            Assert.NotNull(response);
            Assert.Empty(response.Choices);
        }
    }
}
