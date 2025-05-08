using System.Collections.Generic;

namespace OpenRouter.Abstractions
{
    /// <summary>
    /// Encapsulates all options for an HTTP request.
    /// </summary>
    public class HttpRequestOptions
    {
        /// <summary>
        /// Gets or sets the request URL.
        /// </summary>
        public string Url { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the HTTP operation (GET, POST, etc.).
        /// </summary>
        public HttpOperation Operation { get; set; }

        /// <summary>
        /// Gets or sets the collection of headers.
        /// </summary>
        public List<HttpHeader> Headers { get; set; } = new List<HttpHeader>();

        /// <summary>
        /// Gets or sets the collection of query parameters.
        /// </summary>
        public List<HttpParameter> QueryParameters { get; set; } = new List<HttpParameter>();

        /// <summary>
        /// Gets or sets the collection of body parameters (for form-encoded).
        /// </summary>
        public List<HttpParameter> BodyParameters { get; set; } = new List<HttpParameter>();

        /// <summary>
        /// Gets or sets the raw body content (for JSON, etc.).
        /// </summary>
        public string? Body { get; set; }

        /// <summary>
        /// Gets or sets the content type (e.g., application/json).
        /// </summary>
        public string? ContentType { get; set; }

        /// <summary>
        /// Gets or sets the request timeout in seconds.
        /// </summary>
        public int TimeoutSeconds { get; set; } = 100;
    }
}
