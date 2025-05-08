using System;
using OpenRouter.Abstractions;
using Xunit;

namespace OpenRouter.Abstractions.Tests
{
    public class SerializerAbstractionTests
    {
        private class DummySerializer : ISerializer
        {
            public string Serialize<T>(T value) => $"Serialized:{value}";
            public T Deserialize<T>(string json) => (T)(object)$"Deserialized:{json}";
            public object Deserialize(string json, Type type) => $"Deserialized:{json}:{type.Name}";
        }

        private class DummyStringSerializer : ISerializer<string>
        {
            public string Serialize(string value) => $"Serialized:{value}";
            public string Deserialize(string json) => $"Deserialized:{json}";
        }

        [Fact]
        public void ISerializer_Serialize_And_Deserialize_Works()
        {
            var serializer = new DummySerializer();
            var serialized = serializer.Serialize(123);
            Assert.Equal("Serialized:123", serialized);
            var deserialized = serializer.Deserialize<string>("abc");
            Assert.Equal("Deserialized:abc", deserialized);
            var obj = serializer.Deserialize("xyz", typeof(int));
            Assert.Equal("Deserialized:xyz:Int32", obj);
        }

        [Fact]
        public void ISerializerT_Serialize_And_Deserialize_Works()
        {
            var serializer = new DummyStringSerializer();
            var serialized = serializer.Serialize("foo");
            Assert.Equal("Serialized:foo", serialized);
            var deserialized = serializer.Deserialize("bar");
            Assert.Equal("Deserialized:bar", deserialized);
        }
    }
}
