using System;

namespace OpenRouter.Abstractions
{
    /// <summary>
    /// Provides an event notification system for OpenRouter client operations.
    /// </summary>
    public interface IOpenRouterEventNotifier
    {
        /// <summary>
        /// Occurs when a request is sent to the API.
        /// </summary>
        event EventHandler<RequestEventArgs>? RequestSent;

        /// <summary>
        /// Occurs when a response is received from the API.
        /// </summary>
        event EventHandler<ResponseEventArgs>? ResponseReceived;

        /// <summary>
        /// Occurs when a streaming update is received.
        /// </summary>
        event EventHandler<StreamingEventArgs>? StreamingUpdate;

        /// <summary>
        /// Occurs when an error occurs during a client operation.
        /// </summary>
        event EventHandler<ErrorEventArgs>? ErrorOccurred;

        /// <summary>
        /// Occurs to report progress of a long-running operation.
        /// </summary>
        event EventHandler<ProgressEventArgs>? ProgressChanged;
    }
}
