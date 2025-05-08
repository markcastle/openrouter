using System;
using OpenRouter.Abstractions;
using OpenRouter.Client.DependencyInjection;
using Xunit;

namespace OpenRouter.Client.Core.Tests
{
    public class SerializerFactoryTests
    {
        [Theory]
        [InlineData("SystemTextJson", typeof(OpenRouter.Client.SystemTextJson.SystemTextJsonSerializer))]
        [InlineData("systemtextjson", typeof(OpenRouter.Client.SystemTextJson.SystemTextJsonSerializer))]
        [InlineData("NewtonsoftJson", typeof(OpenRouter.Client.NewtonsoftJson.NewtonsoftJsonSerializer))]
        [InlineData("newtonsoftjson", typeof(OpenRouter.Client.NewtonsoftJson.NewtonsoftJsonSerializer))]
        public void Factory_Returns_Correct_Serializer_Type(string type, Type expectedType)
        {
            var serializer = SerializerFactory.Create(type);
            Assert.IsType(expectedType, serializer);
        }

        [Fact]
        public void Factory_Throws_On_Unknown_Type()
        {
            Assert.Throws<ArgumentException>(() => SerializerFactory.Create("unknown"));
        }

        [Fact]
        public void Factory_Throws_On_Empty_Type()
        {
            Assert.Throws<ArgumentException>(() => SerializerFactory.Create(string.Empty));
        }
    }
}
