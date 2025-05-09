using System;
using System.Text;
using Moq;
using OpenRouter.Abstractions;
using OpenRouter.Client.SystemTextJson;
using Xunit;

namespace OpenRouter.Client.SystemTextJson.Tests
{
    public class JsonContentTypeHandlerTests
    {
        [Fact]
        public void ContentType_IsApplicationJson()
        {
            var handler = new JsonContentTypeHandler(Mock.Of<ISerializer>());
            Assert.Equal("application/json", handler.ContentType);
        }

        [Fact]
        public void Serialize_ReturnsUtf8JsonBytes()
        {
            var serializer = new Mock<ISerializer>();
            serializer.Setup(s => s.Serialize(It.IsAny<object>())).Returns("{\"foo\":1}");
            var handler = new JsonContentTypeHandler(serializer.Object);
            var result = handler.Serialize(new { foo = 1 }, "application/json");
            Assert.Equal("{\"foo\":1}", result);
        }

        [Fact]
        public void Serialize_Null_ReturnsEmptyArray()
        {
            var handler = new JsonContentTypeHandler(Mock.Of<ISerializer>());
            Assert.Equal("", handler.Serialize("", "application/json"));
        }

        [Fact]
        public void Deserialize_ReturnsDeserializedObject()
        {
            var serializer = new Mock<ISerializer>();
            serializer.Setup(s => s.Deserialize(It.IsAny<string>(), typeof(int))).Returns(42);
            var handler = new JsonContentTypeHandler(serializer.Object);
            var result = handler.Deserialize("42", typeof(int), "application/json");
            Assert.Equal(42, result);
        }

        [Fact]
        public void Deserialize_Empty_ReturnsDefault()
        {
            var handler = new JsonContentTypeHandler(Mock.Of<ISerializer>());
            Assert.Equal(0, handler.Deserialize("", typeof(int), "application/json"));
        }
    }
}
