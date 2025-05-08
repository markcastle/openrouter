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
        public void Deserialize_InvalidJson_ThrowsOpenRouterSerializationExceptionWithJsonExceptionInner()
        {
            var serializer = new NewtonsoftJsonSerializer();
            var ex = Assert.Throws<OpenRouter.Abstractions.OpenRouterSerializationException>(() => serializer.Deserialize<SimpleModel>("not valid json"));
            Assert.NotNull(ex.InnerException);
            Assert.IsAssignableFrom<Newtonsoft.Json.JsonException>(ex.InnerException);
        }

        [Fact]
        public void Streaming_Serialize_And_Deserialize_RoundTrip_Works()
        {
            var serializer = new NewtonsoftJsonSerializer();
            var model = new SimpleModel { Id = 7, Name = "stream" };
            using var ms = new System.IO.MemoryStream();
            serializer.Serialize(model, ms);
            ms.Position = 0;
            var result = serializer.Deserialize<SimpleModel>(ms);
            Assert.Equal(7, result.Id);
            Assert.Equal("stream", result.Name);
        }

        [Fact]
        public void Streaming_Serialize_Null_ReturnsNullJson()
        {
            var serializer = new NewtonsoftJsonSerializer();
            string? value = null;
            using var ms = new System.IO.MemoryStream();
            serializer.Serialize(value, ms);
            ms.Position = 0;
            using var reader = new System.IO.StreamReader(ms);
            var json = reader.ReadToEnd();
            Assert.Equal("null", json);
        }

        [Fact]
        public void Streaming_Deserialize_InvalidJson_ThrowsOpenRouterSerializationExceptionWithJsonExceptionInner()
        {
            var serializer = new NewtonsoftJsonSerializer();
            using var ms = new System.IO.MemoryStream(System.Text.Encoding.UTF8.GetBytes("not valid json"));
            var ex = Assert.Throws<OpenRouter.Abstractions.OpenRouterSerializationException>(() => serializer.Deserialize<SimpleModel>(ms));
            Assert.NotNull(ex.InnerException);
            Assert.IsAssignableFrom<Newtonsoft.Json.JsonException>(ex.InnerException);
        }

        [Fact]
        public async System.Threading.Tasks.Task Streaming_Async_Serialize_And_Deserialize_RoundTrip_Works()
        {
            var serializer = new NewtonsoftJsonSerializer();
            var model = new SimpleModel { Id = 99, Name = "async" };
            using var ms = new System.IO.MemoryStream();
            await serializer.SerializeAsync(model, ms);
            ms.Position = 0;
            var result = await serializer.DeserializeAsync<SimpleModel>(ms);
            Assert.Equal(99, result.Id);
            Assert.Equal("async", result.Name);
        }

        [Fact]
        public async System.Threading.Tasks.Task Streaming_Async_Serialize_Null_ReturnsNullJson()
        {
            var serializer = new NewtonsoftJsonSerializer();
            string? value = null;
            using var ms = new System.IO.MemoryStream();
            await serializer.SerializeAsync(value, ms);
            ms.Position = 0;
            using var reader = new System.IO.StreamReader(ms);
            var json = await reader.ReadToEndAsync();
            Assert.Equal("null", json);
        }

        [Fact]
        public async System.Threading.Tasks.Task Streaming_Async_Deserialize_InvalidJson_ThrowsOpenRouterSerializationExceptionWithJsonExceptionInner()
        {
            var serializer = new NewtonsoftJsonSerializer();
            using var ms = new System.IO.MemoryStream(System.Text.Encoding.UTF8.GetBytes("not valid json"));
            var ex = await Assert.ThrowsAsync<OpenRouter.Abstractions.OpenRouterSerializationException>(async () => await serializer.DeserializeAsync<SimpleModel>(ms));
            Assert.NotNull(ex.InnerException);
            Assert.IsAssignableFrom<Newtonsoft.Json.JsonException>(ex.InnerException);
        }

    }
}