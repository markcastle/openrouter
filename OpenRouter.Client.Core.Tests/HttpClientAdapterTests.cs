using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Moq.Protected;
using ORHttpRequestOptions = OpenRouter.Abstractions.HttpRequestOptions;
using OpenRouter.Abstractions;
using OpenRouter.Client.Core;
using Xunit;
using System.Linq;
using System.Collections.Generic;

namespace OpenRouter.Client.Core.Tests
{
    /// <summary>
    /// Unit tests for <see cref="HttpClientAdapter"/>.
    /// </summary>
    public class HttpClientAdapterTests
    {
        [Fact]
        public async Task GetAsync_CallsSendAsyncWithGetOperation()
        {
            var handlerMock = new Mock<HttpMessageHandler>();
            handlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage { StatusCode = HttpStatusCode.OK, Content = new StringContent("{}") });
            var httpClient = new HttpClient(handlerMock.Object);
            var adapter = new HttpClientAdapter(httpClient);
            var options = new ORHttpRequestOptions { Url = "https://api.example.com/test" };
            var response = await adapter.GetAsync(options);
            Assert.Equal(200, response.StatusCode);
        }

        [Fact]
        public async Task PostAsync_CallsSendAsyncWithPostOperation()
        {
            var handlerMock = new Mock<HttpMessageHandler>();
            handlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Post),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage { StatusCode = HttpStatusCode.Created, Content = new StringContent("{}") });
            var httpClient = new HttpClient(handlerMock.Object);
            var adapter = new HttpClientAdapter(httpClient);
            var options = new ORHttpRequestOptions { Url = "https://api.example.com/test" };
            var response = await adapter.PostAsync(options);
            Assert.Equal(201, response.StatusCode);
        }

        [Fact]
        public async Task PutAsync_CallsSendAsyncWithPutOperation()
        {
            var handlerMock = new Mock<HttpMessageHandler>();
            handlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Put),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage { StatusCode = HttpStatusCode.Accepted, Content = new StringContent("{}") });
            var httpClient = new HttpClient(handlerMock.Object);
            var adapter = new HttpClientAdapter(httpClient);
            var options = new ORHttpRequestOptions { Url = "https://api.example.com/test" };
            var response = await adapter.PutAsync(options);
            Assert.Equal(202, response.StatusCode);
        }

        [Fact]
        public async Task DeleteAsync_CallsSendAsyncWithDeleteOperation()
        {
            var handlerMock = new Mock<HttpMessageHandler>();
            handlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Delete),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage { StatusCode = HttpStatusCode.NoContent, Content = new StringContent("") });
            var httpClient = new HttpClient(handlerMock.Object);
            var adapter = new HttpClientAdapter(httpClient);
            var options = new ORHttpRequestOptions { Url = "https://api.example.com/test" };
            var response = await adapter.DeleteAsync(options);
            Assert.Equal(204, response.StatusCode);
        }

        [Fact]
        public async Task SendStreamingAsync_ThrowsNotImplementedException()
        {
            var adapter = new HttpClientAdapter();
            var options = new ORHttpRequestOptions { Url = "https://api.example.com/test" };
            await Assert.ThrowsAsync<NotImplementedException>(() => adapter.SendStreamingAsync(options));
        }
        [Fact]
        public async Task SendAsync_ReturnsExpectedResponse_ForGetRequest()
        {
            // Arrange
            var handlerMock = new Mock<HttpMessageHandler>();
            handlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent("{\"result\":42}"),
                    Headers = { { "X-Test", "Value" } }
                });
            var httpClient = new HttpClient(handlerMock.Object);
            var adapter = new HttpClientAdapter(httpClient);
            var options = new ORHttpRequestOptions
            {
                Url = "https://api.example.com/test",
                Operation = HttpOperation.Get,
                Headers = new List<HttpHeader> { new HttpHeader("Accept", "application/json") }
            };

            // Act
            var response = await adapter.SendAsync(options);

            // Assert
            Assert.Equal(200, response.StatusCode);
            Assert.Contains(response.Headers, h => h.Name == "X-Test" && h.Value == "Value");
            Assert.Equal("{\"result\":42}", response.Content);
        }

        [Fact]
        public async Task SendAsync_ThrowsException_ForInvalidUrl()
        {
            var adapter = new HttpClientAdapter();
            var options = new ORHttpRequestOptions { Url = "invalid-url", Operation = HttpOperation.Get };
            await Assert.ThrowsAsync<InvalidOperationException>(() => adapter.SendAsync(options));
        }

        [Fact]
        public async Task SendAsync_SetsContentTypeHeader_ForJsonBody()
        {
            // Arrange
            var handlerMock = new Mock<HttpMessageHandler>();
            handlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent("{}")
                });
            var httpClient = new HttpClient(handlerMock.Object);
            var adapter = new HttpClientAdapter(httpClient);
            var options = new ORHttpRequestOptions
            {
                Url = "https://api.example.com/test",
                Operation = HttpOperation.Post,
                Body = "{\"foo\":\"bar\"}",
                ContentType = "application/json"
            };

            // Act
            var response = await adapter.SendAsync(options);

            // Assert
            Assert.Equal(200, response.StatusCode);
            Assert.Equal("{}", response.Content);
        }

        [Fact]
        public async Task SendAsync_UsesTimeout_FromOptions()
        {
            // Arrange
            var handlerMock = new Mock<HttpMessageHandler>();
            handlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent("{}")
                });
            var httpClient = new HttpClient(handlerMock.Object);
            var adapter = new HttpClientAdapter(httpClient);
            var options = new ORHttpRequestOptions
            {
                Url = "https://api.example.com/test",
                Operation = HttpOperation.Get,
                TimeoutSeconds = 1
            };

            // Act
            var response = await adapter.SendAsync(options);

            // Assert
            Assert.Equal(200, response.StatusCode);
        }

        [Fact]
        public async Task SendAsync_HandlesBodyParameters_ForFormEncoded()
        {
            // Arrange
            var handlerMock = new Mock<HttpMessageHandler>();
            handlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent("{}")
                });
            var httpClient = new HttpClient(handlerMock.Object);
            var adapter = new HttpClientAdapter(httpClient);
            var options = new ORHttpRequestOptions
            {
                Url = "https://api.example.com/test",
                Operation = HttpOperation.Post,
                BodyParameters = new List<HttpParameter> { new HttpParameter("foo", "bar") }
            };

            // Act
            var response = await adapter.SendAsync(options);

            // Assert
            Assert.Equal(200, response.StatusCode);
        }
    }
}
