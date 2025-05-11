using System;
using System.Net.Http;
using OpenRouter.Abstractions;

namespace OpenRouter.Client.Core
{
    /// <summary>
    /// Fluent builder for configuring and creating OpenRouterClient instances.
    /// </summary>
    public class OpenRouterClientBuilder
    {
        private string? _apiKey;
        private string? _baseUrl;
        private IHttpClientAdapter? _httpClientAdapter;
        private ISerializer? _serializer;
        private int? _timeoutSeconds;

        /// <summary>
        /// Sets the API key for authentication.
        /// </summary>
        /// <param name="apiKey">The API key.</param>
        /// <returns>The builder instance.</returns>
        public OpenRouterClientBuilder WithApiKey(string apiKey)
        {
            _apiKey = apiKey;
            return this;
        }

        /// <summary>
        /// Sets the base URL for the API.
        /// </summary>
        /// <param name="baseUrl">The base URL.</param>
        /// <returns>The builder instance.</returns>
        public OpenRouterClientBuilder WithBaseUrl(string baseUrl)
        {
            _baseUrl = baseUrl;
            return this;
        }

        /// <summary>
        /// Sets the HTTP client adapter.
        /// </summary>
        /// <param name="httpClientAdapter">The HTTP client adapter.</param>
        /// <returns>The builder instance.</returns>
        public OpenRouterClientBuilder WithHttpClient(IHttpClientAdapter httpClientAdapter)
        {
            _httpClientAdapter = httpClientAdapter;
            return this;
        }

        /// <summary>
        /// Sets the serializer.
        /// </summary>
        /// <param name="serializer">The serializer implementation.</param>
        /// <returns>The builder instance.</returns>
        public OpenRouterClientBuilder WithSerializer(ISerializer serializer)
        {
            _serializer = serializer;
            return this;
        }

        /// <summary>
        /// Sets the request timeout in seconds.
        /// </summary>
        /// <param name="timeoutSeconds">The timeout duration in seconds.</param>
        /// <returns>The builder instance.</returns>
        public OpenRouterClientBuilder WithTimeout(int timeoutSeconds)
        {
            _timeoutSeconds = timeoutSeconds;
            return this;
        }

        /// <summary>
        /// Sets the request timeout with a TimeSpan.
        /// </summary>
        /// <param name="timeout">The timeout duration as a TimeSpan.</param>
        /// <returns>The builder instance.</returns>
        public OpenRouterClientBuilder WithTimeout(TimeSpan timeout)
        {
            _timeoutSeconds = (int)timeout.TotalSeconds;
            return this;
        }

        /// <summary>
        /// Builds a new OpenRouterClientOptions instance using the configured options.
        /// </summary>
        /// <returns>An OpenRouterClientOptions instance.</returns>
        public OpenRouterClientOptions Build()
        {
            // Reason: Ensures required dependencies are provided and configures the options for flexible usage.
            if (string.IsNullOrWhiteSpace(_apiKey))
            {
                throw new InvalidOperationException("API key must be provided.");
            }

            if (string.IsNullOrWhiteSpace(_baseUrl))
            {
                throw new InvalidOperationException("Base URL must be provided.");
            }

            if (_httpClientAdapter == null)
            {
                throw new InvalidOperationException("HttpClientAdapter must be provided.");
            }

            if (_serializer == null)
            {
                throw new InvalidOperationException("Serializer must be provided.");
            }

            var options = new OpenRouterClientOptions
            {
                Authentication = new AuthenticationOptions { ApiKey = _apiKey },
                Http = new HttpOptions { BaseUrl = _baseUrl, TimeoutSeconds = _timeoutSeconds ?? 100 }
            };

            // Note: _httpClientAdapter and _serializer would be passed to the actual client, not options, when implemented.
            return options;
        }
    }
}
