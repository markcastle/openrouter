using System;

namespace OpenRouter.Abstractions
{
    /// <summary>
    /// Event arguments for a response received from the API.
    /// </summary>
    public class ResponseEventArgs : OpenRouterEventArgs
    {
        /// <summary>
        /// Gets the response model received from the API.
        /// </summary>
        public IResponseModel Response { get; }


        /// <summary>
        /// Initializes a new instance of <see cref="ResponseEventArgs"/>.
        /// </summary>
        /// <param name="response">The response model.</param>
        public ResponseEventArgs(IResponseModel response)
        {
            Response = response;
        }
    }
}
