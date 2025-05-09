using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using OpenRouter.Abstractions;
using System.Linq;
using System.Collections.Generic;

namespace OpenRouter.Client.Core
{
    /// <summary>
    /// Standard implementation of IHttpClientAdapter using System.Net.Http.HttpClient.
    /// </summary>
    public class HttpClientAdapter : IHttpClientAdapter
    {
        /// <summary>
        /// Sends a GET request.
        /// </summary>
        public Task<HttpResponseWrapper> GetAsync(HttpRequestOptions options, CancellationToken cancellationToken = default)
        {
            options.Operation = HttpOperation.Get;
            return SendAsync(options, cancellationToken);
        }

        /// <summary>
        /// Sends a POST request.
        /// </summary>
        public Task<HttpResponseWrapper> PostAsync(HttpRequestOptions options, CancellationToken cancellationToken = default)
        {
            options.Operation = HttpOperation.Post;
            return SendAsync(options, cancellationToken);
        }

        /// <summary>
        /// Sends a PUT request.
        /// </summary>
        public Task<HttpResponseWrapper> PutAsync(HttpRequestOptions options, CancellationToken cancellationToken = default)
        {
            options.Operation = HttpOperation.Put;
            return SendAsync(options, cancellationToken);
        }

        /// <summary>
        /// Sends a DELETE request.
        /// </summary>
        public Task<HttpResponseWrapper> DeleteAsync(HttpRequestOptions options, CancellationToken cancellationToken = default)
        {
            options.Operation = HttpOperation.Delete;
            return SendAsync(options, cancellationToken);
        }

        /// <summary>
        /// Sends a streaming request (not yet implemented).
        /// </summary>
        public Task<HttpResponseWrapper> SendStreamingAsync(HttpRequestOptions options, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException("Streaming is not yet supported.");
        }
        private readonly HttpClient _httpClient;

        /// <summary>
        /// Initializes a new instance of <see cref="HttpClientAdapter"/>.
        /// </summary>
        /// <param name="httpClient">The HttpClient instance to use.</param>
        public HttpClientAdapter(HttpClient? httpClient = null)
        {
            _httpClient = httpClient ?? new HttpClient();
        }

        /// <inheritdoc/>
        public async Task<HttpResponseWrapper> SendAsync(HttpRequestOptions options, CancellationToken cancellationToken = default)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));
            using var request = new HttpRequestMessage(ConvertMethod(options.Operation), options.Url);

            // Set headers
            foreach (var header in options.Headers)
            {
                if (!request.Headers.TryAddWithoutValidation(header.Name, header.Value))
                {
                    // If not a default header, try adding to content headers
                    request.Content ??= new StringContent("");
                    request.Content.Headers.TryAddWithoutValidation(header.Name, header.Value);
                }
            }

            // Set query parameters
            if (options.QueryParameters.Any())
            {
                var uriBuilder = new UriBuilder(options.Url);
                var query = System.Web.HttpUtility.ParseQueryString(uriBuilder.Query);
                foreach (var param in options.QueryParameters)
                {
                    query[param.Name] = param.Value;
                }
                uriBuilder.Query = query.ToString();
                request.RequestUri = uriBuilder.Uri;
            }

            // Set content
            if (!string.IsNullOrEmpty(options.Body))
            {
                request.Content = new StringContent(options.Body);
                if (!string.IsNullOrEmpty(options.ContentType))
                    request.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(options.ContentType);
            }
            else if (options.BodyParameters.Any())
            {
                var formContent = new FormUrlEncodedContent(options.BodyParameters.ToDictionary(p => p.Name, p => p.Value));
                request.Content = formContent;
            }

            // Set timeout
            _httpClient.Timeout = TimeSpan.FromSeconds(options.TimeoutSeconds);

            using var response = await _httpClient.SendAsync(request, cancellationToken).ConfigureAwait(false);
            var content = response.Content != null ? await response.Content.ReadAsStringAsync() : null;

            return new HttpResponseWrapper
            {
                StatusCode = (int)response.StatusCode,
                Headers = response.Headers
                    .SelectMany(h => h.Value.Select(v => new HttpHeader(h.Key, v)))
                    .Concat(
                        response.Content?.Headers != null
                            ? response.Content.Headers.SelectMany(h => h.Value.Select(v => new HttpHeader(h.Key, v)))
                            : Enumerable.Empty<HttpHeader>()
                    ).ToList(),
                Content = content,
                ContentType = response.Content?.Headers.ContentType?.MediaType
            };
        }

        /// <summary>
        /// Converts HttpOperation enum to HttpMethod.
        /// </summary>
        /// <param name="operation">The HTTP operation.</param>
        /// <returns>HttpMethod instance.</returns>
        private static HttpMethod ConvertMethod(HttpOperation operation)
        {
            return operation switch
            {
                HttpOperation.Get => HttpMethod.Get,
                HttpOperation.Post => HttpMethod.Post,
                HttpOperation.Put => HttpMethod.Put,
                HttpOperation.Delete => HttpMethod.Delete,
                HttpOperation.Patch => HttpMethod.Patch,
                HttpOperation.Head => HttpMethod.Head,
                HttpOperation.Options => HttpMethod.Options,
                _ => throw new NotSupportedException($"HTTP operation '{operation}' is not supported.")
            };
        }
    }
}
