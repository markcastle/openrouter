using System;

namespace OpenRouter.Abstractions
{
    /// <summary>
    /// Event arguments for a request being sent to the API.
    /// </summary>
    public class RequestEventArgs : OpenRouterEventArgs
    {
        /// <summary>
        /// Gets the request model sent to the API.
        /// </summary>
        public IRequestModel Request { get; }


        /// <summary>
        /// Initializes a new instance of <see cref="RequestEventArgs"/>.
        /// </summary>
        /// <param name="request">The request model.</param>
        public RequestEventArgs(IRequestModel request)
        {
            Request = request;
        }
    }
}
