using System;
using OpenRouter.Abstractions;
using Xunit;

namespace OpenRouter.Abstractions.Tests
{
    /// <summary>
    /// Tests for mocking OpenRouter abstractions interfaces.
    /// </summary>
    public class AbstractionsInterfaceTests
    {
        /// <summary>
        /// Tests the default constructor of OpenRouterException.
        /// </summary>
        /// <remarks>
        /// Verifies that the default constructor creates a valid instance of OpenRouterException.
        /// </remarks>
        [Fact]
        public void DefaultConstructor_ShouldCreateInstance()
        {
            var ex = new OpenRouterException();
            Assert.NotNull(ex);
        }

        /// <summary>
        /// Tests the constructor of OpenRouterException with a message.
        /// </summary>
        /// <remarks>
        /// Verifies that the constructor sets the message property correctly.
        /// </remarks>
        [Fact]
        public void MessageConstructor_ShouldSetMessage()
        {
            var ex = new OpenRouterException("error");
            Assert.Equal("error", ex.Message);
        }

        /// <summary>
        /// Tests the constructor of OpenRouterException with an inner exception.
        /// </summary>
        /// <remarks>
        /// Verifies that the constructor sets the inner exception property correctly.
        /// </remarks>
        [Fact]
        public void InnerExceptionConstructor_ShouldSetInnerException()
        {
            var inner = new Exception("inner");
            var ex = new OpenRouterException("error", inner);
            Assert.Equal("error", ex.Message);
            Assert.Equal(inner, ex.InnerException);
        }
    }

    /// <summary>
    /// Tests for OpenRouterApiException.
    /// </summary>
    public class OpenRouterApiExceptionTests
    {
        /// <summary>
        /// Tests the default constructor of OpenRouterApiException.
        /// </summary>
        /// <remarks>
        /// Verifies that the default constructor creates a valid instance of OpenRouterApiException.
        /// </remarks>
        [Fact]
        public void DefaultConstructor_ShouldCreateInstance()
        {
            var ex = new OpenRouterApiException();
            Assert.NotNull(ex);
        }

        /// <summary>
        /// Tests the constructor of OpenRouterApiException with a message.
        /// </summary>
        /// <remarks>
        /// Verifies that the constructor sets the message property correctly.
        /// </remarks>
        [Fact]
        public void MessageConstructor_ShouldSetMessage()
        {
            var ex = new OpenRouterApiException("api error");
            Assert.Equal("api error", ex.Message);
        }

        /// <summary>
        /// Tests the constructor of OpenRouterApiException with an inner exception.
        /// </summary>
        /// <remarks>
        /// Verifies that the constructor sets the inner exception property correctly.
        /// </remarks>
        [Fact]
        public void InnerExceptionConstructor_ShouldSetInnerException()
        {
            var inner = new Exception("inner");
            var ex = new OpenRouterApiException("api error", inner);
            Assert.Equal("api error", ex.Message);
            Assert.Equal(inner, ex.InnerException);
        }
    }

    /// <summary>
    /// Tests for OpenRouterAuthenticationException.
    /// </summary>
    public class OpenRouterAuthenticationExceptionTests
    {
        /// <summary>
        /// Tests the default constructor of OpenRouterAuthenticationException.
        /// </summary>
        /// <remarks>
        /// Verifies that the default constructor creates a valid instance of OpenRouterAuthenticationException.
        /// </remarks>
        [Fact]
        public void DefaultConstructor_ShouldCreateInstance()
        {
            var ex = new OpenRouterAuthenticationException();
            Assert.NotNull(ex);
        }

        /// <summary>
        /// Tests the message constructor of OpenRouterAuthenticationException.
        /// </summary>
        [Fact]
        public void MessageConstructor_ShouldSetMessage()
        {
            var ex = new OpenRouterAuthenticationException("auth error");
            Assert.Equal("auth error", ex.Message);
        }

                /// <summary>
        /// Tests the inner exception constructor of OpenRouterAuthenticationException.
        /// </summary>
        [Fact]
        public void InnerExceptionConstructor_ShouldSetInnerException()
        {
            var inner = new Exception("inner");
            var ex = new OpenRouterAuthenticationException("auth error", inner);
            Assert.Equal("auth error", ex.Message);
            Assert.Equal(inner, ex.InnerException);
        }
    }

    /// <summary>
    /// Tests for OpenRouterRateLimitException.
    /// </summary>
    public class OpenRouterRateLimitExceptionTests
    {
        [Fact]
        public void DefaultConstructor_ShouldCreateInstance()
        {
            var ex = new OpenRouterRateLimitException();
            Assert.NotNull(ex);
        }

        [Fact]
        public void MessageConstructor_ShouldSetMessage()
        {
            var ex = new OpenRouterRateLimitException("rate limit");
            Assert.Equal("rate limit", ex.Message);
        }

        [Fact]
        public void InnerExceptionConstructor_ShouldSetInnerException()
        {
            var inner = new Exception("inner");
            var ex = new OpenRouterRateLimitException("rate limit", inner);
            Assert.Equal("rate limit", ex.Message);
            Assert.Equal(inner, ex.InnerException);
        }
    }

    /// <summary>
    /// Tests for OpenRouterTimeoutException.
    /// </summary>
    public class OpenRouterTimeoutExceptionTests
    {
        [Fact]
        public void DefaultConstructor_ShouldCreateInstance()
        {
            var ex = new OpenRouterTimeoutException();
            Assert.NotNull(ex);
        }

        [Fact]
        public void MessageConstructor_ShouldSetMessage()
        {
            var ex = new OpenRouterTimeoutException("timeout");
            Assert.Equal("timeout", ex.Message);
        }

        [Fact]
        public void InnerExceptionConstructor_ShouldSetInnerException()
        {
            var inner = new Exception("inner");
            var ex = new OpenRouterTimeoutException("timeout", inner);
            Assert.Equal("timeout", ex.Message);
            Assert.Equal(inner, ex.InnerException);
        }
    }

    /// <summary>
    /// Tests for OpenRouterNetworkException.
    /// </summary>
    public class OpenRouterNetworkExceptionTests
    {
        [Fact]
        public void DefaultConstructor_ShouldCreateInstance()
        {
            var ex = new OpenRouterNetworkException();
            Assert.NotNull(ex);
        }

        [Fact]
        public void MessageConstructor_ShouldSetMessage()
        {
            var ex = new OpenRouterNetworkException("network");
            Assert.Equal("network", ex.Message);
        }

        [Fact]
        public void InnerExceptionConstructor_ShouldSetInnerException()
        {
            var inner = new Exception("inner");
            var ex = new OpenRouterNetworkException("network", inner);
            Assert.Equal("network", ex.Message);
            Assert.Equal(inner, ex.InnerException);
        }
    }

    /// <summary>
    /// Tests for OpenRouterSerializationException.
    /// </summary>
    public class OpenRouterSerializationExceptionTests
    {
        [Fact]
        public void DefaultConstructor_ShouldCreateInstance()
        {
            var ex = new OpenRouterSerializationException();
            Assert.NotNull(ex);
        }

        [Fact]
        public void MessageConstructor_ShouldSetMessage()
        {
            var ex = new OpenRouterSerializationException("serialization");
            Assert.Equal("serialization", ex.Message);
        }

        [Fact]
        public void InnerExceptionConstructor_ShouldSetInnerException()
        {
            var inner = new Exception("inner");
            var ex = new OpenRouterSerializationException("serialization", inner);
            Assert.Equal("serialization", ex.Message);
            Assert.Equal(inner, ex.InnerException);
        }
    }
}