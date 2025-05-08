namespace OpenRouter.Client.NewtonsoftJson.Tests
{
    /// <summary>
    /// Basic tests for Newtonsoft.Json integration.
    /// </summary>
    public class NewtonsoftJsonBasicTests
    {
        private class SimpleModel
        {
            public int Id { get; set; }
            public string? Name { get; set; }
        }

        [Fact]
        public void Serialize_And_Deserialize_RoundTrip_Works()
        {
            var serializer = new NewtonsoftJsonSerializer();
            var model = new SimpleModel { Id = 42, Name = "test" };
            var json = serializer.Serialize(model);
            var result = serializer.Deserialize<SimpleModel>(json);
            Assert.Equal(42, result.Id);
            Assert.Equal("test", result.Name);
        }

        [Fact]
        public void Serialize_Null_ReturnsNullJson()
        {
            var serializer = new NewtonsoftJsonSerializer();
            string? value = null;
            var json = serializer.Serialize(value);
            Assert.Equal("null", json);
        }

        [Fact]
        public void Deserialize_InvalidJson_Throws()
        {
            var serializer = new NewtonsoftJsonSerializer();
            Assert.ThrowsAny<Newtonsoft.Json.JsonException>(() => serializer.Deserialize<SimpleModel>("not valid json"));
        }

    }
}