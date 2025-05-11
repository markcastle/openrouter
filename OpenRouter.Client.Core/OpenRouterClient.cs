using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using OpenRouter.Abstractions;
using OpenRouter.Client.Core.Adapters;

namespace OpenRouter.Client.Core
{
    /// <summary>
    /// Minimal implementation of IOpenRouterClient for MVP.
    /// Uses injected IHttpClientAdapter and ISerializer to send requests.
    /// </summary>
    public class OpenRouterClient : IOpenRouterClient
    {
        private readonly IHttpClientAdapter _httpClientAdapter;
        private readonly ISerializer _serializer;
        private readonly OpenRouterClientOptions _options;

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenRouterClient"/> class.
        /// </summary>
        /// <param name="httpClientAdapter">The HTTP client adapter to use.</param>
        /// <param name="serializer">The serializer to use.</param>
        /// <param name="options">The client options.</param>
        public OpenRouterClient(IHttpClientAdapter httpClientAdapter, ISerializer serializer, OpenRouterClientOptions options)
        {
            _httpClientAdapter = httpClientAdapter ?? throw new ArgumentNullException(nameof(httpClientAdapter));
            _serializer = serializer ?? throw new ArgumentNullException(nameof(serializer));
            _options = options ?? throw new ArgumentNullException(nameof(options));
            _options.Validate();
        }

        /// <inheritdoc />
        public async Task<TResponse> SendAsync<TRequest, TResponse>(TRequest request, CancellationToken cancellationToken = default)
        {
            // Reason: MVP - serialize request, send via adapter, and deserialize response.
            var url = _options.Http.BaseUrl?.TrimEnd('/') + "/chat/completions"; // Example endpoint
            var json = _serializer.Serialize(request);
            var httpRequestOptions = new HttpRequestOptions
            {
                Url = url!,
                Operation = HttpOperation.Post,
                Headers = new List<HttpHeader>
                {
                    new HttpHeader { Name = "Authorization", Value = $"Bearer {_options.Authentication.ApiKey}" },
                    new HttpHeader { Name = "Content-Type", Value = "application/json" }
                },
                Body = json,
                ContentType = "application/json",
                TimeoutSeconds = _options.Http.TimeoutSeconds
            };
            var httpResponse = await _httpClientAdapter.SendAsync(httpRequestOptions, cancellationToken);
            // Throw if status code is not 2xx
            if (httpResponse.StatusCode < 200 || httpResponse.StatusCode >= 300)
            {
                // Reason: Ensure API errors are surfaced to the caller
                throw new System.Net.Http.HttpRequestException($"OpenRouter API error: {httpResponse.StatusCode} - {httpResponse.Content}");
            }
            return _serializer.Deserialize<TResponse>(httpResponse.Content!);
        }
        /// <summary>
        /// Calls the OpenRouter chat completions endpoint.
        /// </summary>
        /// <param name="request">The chat completion request.</param>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>The chat completion response.</returns>
        public async Task<ChatCompletionResponse> CreateChatCompletionAsync(ChatCompletionRequest request, CancellationToken cancellationToken = default)
        {
            // Reason: Strongly-typed endpoint for chat completions
            return await SendAsync<ChatCompletionRequest, ChatCompletionResponse>(request, cancellationToken);
        }
    }
}
