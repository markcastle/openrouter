using System.Threading;
using System.Threading.Tasks;

namespace OpenRouter.Abstractions
{
    /// <summary>
    /// Abstraction for an HTTP client adapter.
    /// </summary>
    public interface IHttpClientAdapter
    {
        /// <summary>
        /// Sends an HTTP request asynchronously.
        /// </summary>
        /// <param name="options">The request options.</param>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>The HTTP response wrapper.</returns>
        Task<HttpResponseWrapper> SendAsync(HttpRequestOptions options, CancellationToken cancellationToken = default);
    }
}
