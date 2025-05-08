using System;
using OpenRouter.Abstractions;
using Xunit;

namespace OpenRouter.Abstractions.Tests
{
    /// <summary>
    /// Unit tests for OpenRouter abstractions interfaces.
    /// </summary>
    public class AbstractionsInterfaceTests
    {
        /// <summary>
        /// Tests the default constructor of OpenRouterException.
        /// </summary>
        [Fact]
        public void DefaultConstructor_ShouldCreateInstance()
        {
            var ex = new OpenRouterException();
            Assert.NotNull(ex);
        }

        [Fact]
        public void MessageConstructor_ShouldSetMessage()
        {
            var ex = new OpenRouterException("error");
            Assert.Equal("error", ex.Message);
        }

        [Fact]
        public void InnerExceptionConstructor_ShouldSetInnerException()
        {
            var inner = new Exception("inner");
            var ex = new OpenRouterException("error", inner);
            Assert.Equal("error", ex.Message);
            Assert.Equal(inner, ex.InnerException);
        }
    }

    public class OpenRouterApiExceptionTests
    {
        [Fact]
        public void DefaultConstructor_ShouldCreateInstance()
        {
            var ex = new OpenRouterApiException();
            Assert.NotNull(ex);
        }

        [Fact]
        public void MessageConstructor_ShouldSetMessage()
        {
            var ex = new OpenRouterApiException("api error");
            Assert.Equal("api error", ex.Message);
        }

        [Fact]
        public void InnerExceptionConstructor_ShouldSetInnerException()
        {
            var inner = new Exception("inner");
            var ex = new OpenRouterApiException("api error", inner);
            Assert.Equal("api error", ex.Message);
            Assert.Equal(inner, ex.InnerException);
        }
    }

    public class OpenRouterAuthenticationExceptionTests
    {
        [Fact]
        public void DefaultConstructor_ShouldCreateInstance()
        {
            var ex = new OpenRouterAuthenticationException();
            Assert.NotNull(ex);
        }

        [Fact]
        public void MessageConstructor_ShouldSetMessage()
        {
            var ex = new OpenRouterAuthenticationException("auth error");
            Assert.Equal("auth error", ex.Message);
        }

        [Fact]
        public void InnerExceptionConstructor_ShouldSetInnerException()
        {
            var inner = new Exception("inner");
            var ex = new OpenRouterAuthenticationException("auth error", inner);
            Assert.Equal("auth error", ex.Message);
            Assert.Equal(inner, ex.InnerException);
        }
    }
}