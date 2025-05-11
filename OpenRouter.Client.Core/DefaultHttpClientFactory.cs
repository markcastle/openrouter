using System;
using OpenRouter.Abstractions;

namespace OpenRouter.Client.Core
{
    /// <summary>
    /// Default implementation of IHttpClientFactory for creating and caching IHttpClientAdapter instances.
    /// </summary>
    public class DefaultHttpClientFactory : IHttpClientFactory, IDisposable
    {
        private IHttpClientAdapter? _adapter;
        private bool _disposed;

        /// <summary>
        /// Creates or returns a cached instance of IHttpClientAdapter.
        /// </summary>
        /// <returns>An IHttpClientAdapter instance.</returns>
        public IHttpClientAdapter CreateClient()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(DefaultHttpClientFactory));
            }

            return _adapter ??= new HttpClientAdapter();
        }

        /// <summary>
        /// Disposes the factory and any cached adapters.
        /// </summary>
        public void Dispose()
        {
            if (_disposed)
            {
                return;
            }

            if (_adapter is IDisposable disposable)
            {
                disposable.Dispose();
            }

            _disposed = true;
        }
    }
}
