using System.Net.Http;

namespace OpenRouter.Abstractions
{
    /// <summary>
    /// Provides authentication services for OpenRouter API requests.
    /// </summary>
    public interface IAuthenticationService
    {
        /// <summary>
        /// Adds authentication headers or tokens to an outgoing request.
        /// </summary>
        /// <param name="request">The HTTP request message to modify.</param>
        void Authenticate(HttpRequestMessage request);
    }
}
