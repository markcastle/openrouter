using System.Collections.Generic;

namespace OpenRouter.Abstractions
{
    /// <summary>
    /// Represents a standardized HTTP response.
    /// </summary>
    public class HttpResponseWrapper
    {
        /// <summary>
        /// Gets or sets the HTTP status code.
        /// </summary>
        public int StatusCode { get; set; }

        /// <summary>
        /// Gets or sets the response headers.
        /// </summary>
        public List<HttpHeader> Headers { get; set; } = new List<HttpHeader>();

        /// <summary>
        /// Gets or sets the response content as string.
        /// </summary>
        public string? Content { get; set; }

        /// <summary>
        /// Gets or sets the content type of the response.
        /// </summary>
        public string? ContentType { get; set; }
    }
}
