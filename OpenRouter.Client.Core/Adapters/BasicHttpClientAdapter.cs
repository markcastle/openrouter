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
    public class BasicHttpClientAdapter : IHttpClientAdapter
    {
        private readonly HttpClient _client;
        public BasicHttpClientAdapter(HttpClient? client = null)
        {
            _client = client ?? new HttpClient();
        }

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

            // Add headers
            if (options.Headers != null)
            {
                foreach (var header in options.Headers)
                {
                    request.Headers.Add(header.Name, header.Value);
                }
            }

            // Add body if present
            if (!string.IsNullOrWhiteSpace(options.Body))
            {
                request.Content = new StringContent(options.Body, System.Text.Encoding.UTF8, options.ContentType ?? "application/json");
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
