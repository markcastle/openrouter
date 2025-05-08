namespace OpenRouter.Client.SystemTextJson.Tests;

/// <summary>
/// Basic tests for System.Text.Json integration.
/// </summary>
public class SystemTextJsonBasicTests
{
    private class SimpleModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
    }

    [Fact]
    public void Serialize_And_Deserialize_RoundTrip_Works()
    {
        var serializer = new SystemTextJsonSerializer();
        var model = new SimpleModel { Id = 42, Name = "test" };
        var json = serializer.Serialize(model);
        var result = serializer.Deserialize<SimpleModel>(json);
        Assert.Equal(42, result.Id);
        Assert.Equal("test", result.Name);
    }

    [Fact]
    public void Serialize_Null_ReturnsNullJson()
    {
        var serializer = new SystemTextJsonSerializer();
        string? value = null;
        var json = serializer.Serialize(value);
        Assert.Equal("null", json);
    }

    [Fact]
    public void Deserialize_InvalidJson_ThrowsOpenRouterSerializationExceptionWithJsonExceptionInner()
    {
        var serializer = new SystemTextJsonSerializer();
        var ex = Assert.Throws<OpenRouter.Abstractions.OpenRouterSerializationException>(() => serializer.Deserialize<SimpleModel>("not valid json"));
        Assert.NotNull(ex.InnerException);
        Assert.IsType<System.Text.Json.JsonException>(ex.InnerException);
    }
}