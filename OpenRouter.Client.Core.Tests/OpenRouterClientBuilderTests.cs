using System;
using OpenRouter.Abstractions;
using OpenRouter.Client.Core;
using Xunit;
using Moq;

namespace OpenRouter.Client.Core.Tests
{
    public class OpenRouterClientBuilderTests
    {
        [Fact]
        public void Build_WithAllRequiredParameters_ReturnsOptionsWithCorrectValues()
        {
            // Arrange
            var httpClientAdapter = new Mock<IHttpClientAdapter>().Object;
            var serializer = new Mock<ISerializer>().Object;
            var builder = new OpenRouterClientBuilder()
                .WithApiKey("test-key")
                .WithBaseUrl("https://api.example.com")
                .WithHttpClient(httpClientAdapter)
                .WithSerializer(serializer)
                .WithTimeout(30);

            // Act
            var options = builder.Build();

            // Assert
            Assert.NotNull(options);
            Assert.Equal("test-key", options.Authentication.ApiKey);
            Assert.Equal("https://api.example.com", options.Http.BaseUrl);
            Assert.Equal(30, options.Http.TimeoutSeconds);
        }

        [Fact]
        public void Build_WithoutApiKey_Throws()
        {
            var builder = new OpenRouterClientBuilder()
                .WithBaseUrl("https://api.example.com")
                .WithHttpClient(new Mock<IHttpClientAdapter>().Object)
                .WithSerializer(new Mock<ISerializer>().Object);

            Assert.Throws<InvalidOperationException>(() => builder.Build());
        }

        [Fact]
        public void Build_WithoutBaseUrl_Throws()
        {
            var builder = new OpenRouterClientBuilder()
                .WithApiKey("test-key")
                .WithHttpClient(new Mock<IHttpClientAdapter>().Object)
                .WithSerializer(new Mock<ISerializer>().Object);

            Assert.Throws<InvalidOperationException>(() => builder.Build());
        }

        [Fact]
        public void Build_WithoutHttpClientAdapter_Throws()
        {
            var builder = new OpenRouterClientBuilder()
                .WithApiKey("test-key")
                .WithBaseUrl("https://api.example.com")
                .WithSerializer(new Mock<ISerializer>().Object);

            Assert.Throws<InvalidOperationException>(() => builder.Build());
        }

        [Fact]
        public void Build_WithoutSerializer_Throws()
        {
            var builder = new OpenRouterClientBuilder()
                .WithApiKey("test-key")
                .WithBaseUrl("https://api.example.com")
                .WithHttpClient(new Mock<IHttpClientAdapter>().Object);

            Assert.Throws<InvalidOperationException>(() => builder.Build());
        }
    }
}
