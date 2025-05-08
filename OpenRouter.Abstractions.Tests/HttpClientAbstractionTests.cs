using System;
using System.Threading;
using System.Threading.Tasks;
using OpenRouter.Abstractions;
using Xunit;

namespace OpenRouter.Abstractions.Tests
{
    public class HttpClientAbstractionTests
    {
        [Fact]
        public async Task SendAsync_ReturnsResponse_ForValidRequest()
        {
            // Arrange
            var adapter = new DummyHttpClientAdapter();
            var options = new HttpRequestOptions
            {
                Url = "https://api.example.com/test",
                Operation = HttpOperation.Get
            };
            // Act
            var response = await adapter.SendAsync(options);
            // Assert
            Assert.NotNull(response);
            Assert.True(response.StatusCode >= 200 && response.StatusCode < 300);
        }

        [Fact]
        public async Task SendAsync_ThrowsException_ForInvalidUrl()
        {
            var adapter = new DummyHttpClientAdapter();
            var options = new HttpRequestOptions { Url = "invalid-url", Operation = HttpOperation.Get };
            await Assert.ThrowsAsync<Exception>(() => adapter.SendAsync(options));
        }

        [Fact]
        public void HttpRequestOptions_Defaults_AreValid()
        {
            var options = new HttpRequestOptions();
            Assert.Equal(100, options.TimeoutSeconds);
            Assert.NotNull(options.Headers);
            Assert.NotNull(options.QueryParameters);
            Assert.NotNull(options.BodyParameters);
        }

        // Dummy implementation for testing
        private class DummyHttpClientAdapter : IHttpClientAdapter
        {
            public Task<HttpResponseWrapper> SendAsync(HttpRequestOptions options, CancellationToken cancellationToken = default)
            {
                if (string.IsNullOrWhiteSpace(options.Url) || options.Url.StartsWith("invalid"))
                {
                    throw new Exception("Invalid URL");
                }

                return Task.FromResult(new HttpResponseWrapper
                {
                    StatusCode = 200,
                    Content = "{}",
                    ContentType = "application/json"
                });
            }
        }
    }
}
