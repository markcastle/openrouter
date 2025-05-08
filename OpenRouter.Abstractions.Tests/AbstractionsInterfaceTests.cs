using System.Net.Http;
using System.Threading.Tasks;
using Moq;
using OpenRouter.Abstractions;
using Xunit;

namespace OpenRouter.Abstractions.Tests
{
    public class AbstractionsInterfaceTests
    {
        [Fact]
        public void CanMock_IOpenRouterClient()
        {
            var mock = new Mock<IOpenRouterClient>();
            Assert.NotNull(mock.Object);
        }

        [Fact]
        public async Task CanMock_IHttpTransport_SendAsync()
        {
            var mock = new Mock<IHttpTransport>();
            mock.Setup(m => m.SendAsync(It.IsAny<HttpRequestMessage>())).ReturnsAsync(new HttpResponseMessage());
            var result = await mock.Object.SendAsync(new HttpRequestMessage());
            Assert.IsType<HttpResponseMessage>(result);
        }

        [Fact]
        public void CanMock_IAuthenticationService_Authenticate()
        {
            var mock = new Mock<IAuthenticationService>();
            var req = new HttpRequestMessage();
            mock.Object.Authenticate(req);
            Assert.NotNull(req);
        }
    }
}
