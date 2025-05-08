using System;
using OpenRouter.Abstractions;
using Xunit;

namespace OpenRouter.Abstractions.Tests
{
    public class OptionsValidationTests
    {
        [Fact]
        public void AuthenticationOptions_ThrowsIfNoCredentials()
        {
            var options = new AuthenticationOptions();
            Assert.Throws<OpenRouterAuthenticationException>(() => options.Validate());
        }

        [Fact]
        public void AuthenticationOptions_PassesIfApiKey()
        {
            var options = new AuthenticationOptions { ApiKey = "key" };
            options.Validate();
        }

        [Fact]
        public void AuthenticationOptions_PassesIfBearerToken()
        {
            var options = new AuthenticationOptions { BearerToken = "token" };
            options.Validate();
        }

        [Fact]
        public void HttpOptions_ThrowsIfBaseUrlMissing()
        {
            var options = new HttpOptions { TimeoutSeconds = 10, MaxRetries = 1 };
            Assert.Throws<OpenRouterException>(() => options.Validate());
        }

        [Fact]
        public void HttpOptions_ThrowsIfTimeoutNonPositive()
        {
            var options = new HttpOptions { BaseUrl = "https://api", TimeoutSeconds = 0 };
            Assert.Throws<OpenRouterException>(() => options.Validate());
        }

        [Fact]
        public void HttpOptions_ThrowsIfMaxRetriesNegative()
        {
            var options = new HttpOptions { BaseUrl = "https://api", TimeoutSeconds = 10, MaxRetries = -1 };
            Assert.Throws<OpenRouterException>(() => options.Validate());
        }

        [Fact]
        public void SerializationOptions_ThrowsIfSerializerTypeMissing()
        {
            var options = new SerializationOptions { SerializerType = null };
            Assert.Throws<OpenRouterSerializationException>(() => options.Validate());
        }

        [Fact]
        public void ResilienceOptions_ThrowsIfCircuitBreakerThresholdNegative()
        {
            var options = new ResilienceOptions { CircuitBreakerThreshold = -1 };
            Assert.Throws<OpenRouterException>(() => options.Validate());
        }

        [Fact]
        public void ResilienceOptions_ThrowsIfResetSecondsNonPositive()
        {
            var options = new ResilienceOptions { CircuitBreakerResetSeconds = 0 };
            Assert.Throws<OpenRouterException>(() => options.Validate());
        }

        [Fact]
        public void ProviderRoutingOptions_Validate_DoesNotThrow()
        {
            var options = new ProviderRoutingOptions { PreferredProvider = "test", EnableFallback = true };
            options.Validate();
        }

        [Fact]
        public void OpenRouterClientOptions_Validate_ValidatesAllNested()
        {
            var options = new OpenRouterClientOptions
            {
                Authentication = new AuthenticationOptions { ApiKey = "key" },
                Http = new HttpOptions { BaseUrl = "https://api", TimeoutSeconds = 10, MaxRetries = 1 },
                Serialization = new SerializationOptions { SerializerType = "SystemTextJson" },
                Resilience = new ResilienceOptions { CircuitBreakerThreshold = 1, CircuitBreakerResetSeconds = 10 },
                ProviderRouting = new ProviderRoutingOptions { PreferredProvider = "test", EnableFallback = true }
            };
            options.Validate();
        }
    }
}
