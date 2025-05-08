using OpenRouter.Abstractions;
using Xunit;

namespace OpenRouter.Abstractions.Tests
{
    public class HttpHeaderTests
    {
        [Fact]
        public void Constructor_SetsNameAndValue()
        {
            var header = new HttpHeader("Authorization", "Bearer token");
            Assert.Equal("Authorization", header.Name);
            Assert.Equal("Bearer token", header.Value);
        }
    }
}
