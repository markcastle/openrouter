using OpenRouter.Abstractions;
using Xunit;

namespace OpenRouter.Abstractions.Tests
{
    public class HttpParameterTests
    {
        [Fact]
        public void Constructor_SetsNameAndValue()
        {
            var param = new HttpParameter("query", "value");
            Assert.Equal("query", param.Name);
            Assert.Equal("value", param.Value);
        }
    }
}
