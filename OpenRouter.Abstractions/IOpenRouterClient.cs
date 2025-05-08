using System.Threading;
using System.Threading.Tasks;

namespace OpenRouter.Abstractions
{
    /// <summary>
    /// Abstraction for the main OpenRouter API Client.
    /// </summary>
    public interface IOpenRouterClient
    {
        /// <summary>
        /// Sends a request to the OpenRouter API and returns the response asynchronously.
        /// </summary>
        /// <typeparam name="TRequest">The type of the request object.</typeparam>
        /// <typeparam name="TResponse">The type of the response object.</typeparam>
        /// <param name="request">The request payload to send.</param>
        /// <param name="cancellationToken">A cancellation token for the async operation.</param>
        /// <returns>The response from the API.</returns>
        Task<TResponse> SendAsync<TRequest, TResponse>(TRequest request, CancellationToken cancellationToken = default);
    }
}
