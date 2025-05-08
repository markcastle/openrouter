namespace OpenRouter.Abstractions
{
    /// <summary>
    /// Provides event notification for OpenRouter client operations.
    /// </summary>
    public interface IEventNotifier
    {
        /// <summary>
        /// Publishes an event to all registered subscribers.
        /// </summary>
        /// <typeparam name="TEvent">The type of event data.</typeparam>
        /// <param name="eventData">The event data to publish.</param>
        void Publish<TEvent>(TEvent eventData);
    }
}
