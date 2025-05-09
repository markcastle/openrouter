using System;
using OpenRouter.Abstractions;
using OpenRouter.Client.Core;
using Xunit;

namespace OpenRouter.Client.Core.Tests
{
    public class DefaultHttpClientFactoryTests
    {
        [Fact]
        public void CreateHttpClient_ReturnsSameInstance()
        {
            var factory = new DefaultHttpClientFactory();
            var client1 = factory.CreateClient();
            var client2 = factory.CreateClient();
            Assert.Same(client1, client2);
        }

        [Fact]
        public void CreateHttpClient_ThrowsAfterDispose()
        {
            var factory = new DefaultHttpClientFactory();
            factory.Dispose();
            Assert.Throws<ObjectDisposedException>(() => factory.CreateClient());
        }

        [Fact]
        public void Dispose_MultipleCalls_DoesNotThrow()
        {
            var factory = new DefaultHttpClientFactory();
            factory.Dispose();
            factory.Dispose();
        }
    }
}
