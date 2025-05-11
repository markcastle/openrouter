using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using OpenRouter.Abstractions;

namespace OpenRouter.Client.Core.Adapters
{
    /// <summary>
    /// Basic implementation of IHttpClientAdapter using System.Net.Http.HttpClient.
    /// </summary>
    /// <summary>
    /// Basic implementation of IHttpClientAdapter using System.Net.Http.HttpClient.
    /// </summary>
    public class BasicHttpClientAdapter : IHttpClientAdapter
    {
        private readonly HttpClient _client;
        /// <summary>
        /// Initializes a new instance of the <see cref="BasicHttpClientAdapter"/> class.
        /// </summary>
        /// <param name="client">The HttpClient instance to use. If null, a new HttpClient is created.</param>
        public BasicHttpClientAdapter(HttpClient? client = null)
        {
            _client = client ?? new HttpClient();
        }

        /// <summary>
        /// Sends an HTTP request using the specified options and returns a wrapped response.
        /// </summary>
        /// <param name="options">The HTTP request options.</param>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>A wrapped HTTP response.</returns>
        public async Task<HttpResponseWrapper> SendAsync(HttpRequestOptions options, CancellationToken cancellationToken = default)
        {
            var method = new HttpMethod(options.Operation.ToString());
            var uri = options.Url;
            // Add query parameters if present
            if (options.QueryParameters != null && options.QueryParameters.Count > 0)
            {
                var uriBuilder = new UriBuilder(uri);
                var query = System.Web.HttpUtility.ParseQueryString(uriBuilder.Query);
                foreach (var param in options.QueryParameters)
                {
                    query[param.Name] = param.Value;
                }
                uriBuilder.Query = query.ToString();
                uri = uriBuilder.ToString();
            }
            var request = new HttpRequestMessage(method, uri);

            // Add headers (except Content-Type)
            string? contentTypeOverride = null;
            if (options.Headers != null)
            {
                foreach (var header in options.Headers)
                {
                    if (header.Name.Equals("Content-Type", StringComparison.OrdinalIgnoreCase))
                    {
                        contentTypeOverride = header.Value;
                        continue; // Don't add Content-Type to request.Headers
                    }
                    request.Headers.Add(header.Name, header.Value);
                }
            }

            // Add body if present
            if (!string.IsNullOrWhiteSpace(options.Body))
            {
                var contentType = contentTypeOverride ?? options.ContentType ?? "application/json";
                request.Content = new StringContent(options.Body, System.Text.Encoding.UTF8, contentType);
            }

            var response = await _client.SendAsync(request, cancellationToken);
            var wrapper = new HttpResponseWrapper
            {
                StatusCode = (int)response.StatusCode,
                Content = response.Content != null ? await response.Content.ReadAsStringAsync() : null,
                ContentType = response.Content?.Headers.ContentType?.MediaType,
                Headers = response.Headers != null ?
                    response.Headers.Select(h => new HttpHeader { Name = h.Key, Value = string.Join(",", h.Value) }).ToList() : new List<HttpHeader>()
            };
            return wrapper;
        }

        // (Optional) Keep for internal/backward compatibility
        /// <summary>
        /// Sends a POST request with a JSON body and optional API key.
        /// </summary>
        /// <param name="url">The URL to send the request to.</param>
        /// <param name="json">The JSON content to send.</param>
        /// <param name="apiKey">The API key for authorization (optional).</param>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>The HTTP response message.</returns>
        public async Task<HttpResponseMessage> PostAsync(string url, string json, string? apiKey, CancellationToken cancellationToken)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json")
            };
            if (!string.IsNullOrWhiteSpace(apiKey))
            {
                request.Headers.Add("Authorization", $"Bearer {apiKey}");
            }
            return await _client.SendAsync(request, cancellationToken);
        }
    }
}
