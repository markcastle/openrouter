using System.Net.Http;
using System.Threading.Tasks;

namespace OpenRouter.Abstractions
{
    /// <summary>
    /// Abstraction for the HTTP transport layer, allowing for custom implementations and testing.
    /// </summary>
    public interface IHttpTransport
    {
        /// <summary>
        /// Sends an HTTP request asynchronously.
        /// </summary>
        /// <param name="request">The HTTP request message.</param>
        /// <returns>The HTTP response message.</returns>
        Task<HttpResponseMessage> SendAsync(HttpRequestMessage request);
    }
}
