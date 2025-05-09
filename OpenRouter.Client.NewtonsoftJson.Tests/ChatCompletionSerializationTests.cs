using System.Collections.Generic;
using OpenRouter.Abstractions;
using OpenRouter.Client.NewtonsoftJson;
using Xunit;

namespace OpenRouter.Client.NewtonsoftJson.Tests
{
    public class ChatCompletionSerializationTests
    {
        [Fact]
        public void Serialize_ChatCompletionRequest_ProducesSnakeCaseJson()
        {
            var request = new ChatCompletionRequest
            {
                Model = "gpt-3.5-turbo",
                Messages = new List<Message> { new Message { Role = "user", Content = "Hello!" } },
                Temperature = 0.7f,
                MaxTokens = 128
            };
            var serializer = new NewtonsoftJsonSerializer();
            var json = serializer.Serialize(request);
            Assert.Contains("model", json);
            Assert.Contains("messages", json);
            Assert.Contains("temperature", json);
            Assert.Contains("max_tokens", json); // snake_case
        }

        [Fact]
        public void Deserialize_ChatCompletionRequest_FromSnakeCaseJson_Works()
        {
            var json = "{\"model\":\"gpt-3.5-turbo\",\"messages\":[{\"role\":\"user\",\"content\":\"Hello!\"}],\"temperature\":0.7,\"max_tokens\":128}";
            var serializer = new NewtonsoftJsonSerializer();
            var obj = serializer.Deserialize<ChatCompletionRequest>(json);
            Assert.Equal("gpt-3.5-turbo", obj.Model);
            Assert.Single(obj.Messages);
            Assert.Equal("user", obj.Messages[0].Role);
            Assert.Equal("Hello!", obj.Messages[0].Content);
            Assert.Equal(0.7f, obj.Temperature);
            Assert.Equal(128, obj.MaxTokens);
        }

        [Fact]
        public void Serialize_EmptyRequest_ProducesMinimalJson()
        {
            var request = new ChatCompletionRequest();
            var serializer = new NewtonsoftJsonSerializer();
            var json = serializer.Serialize(request);
            Assert.Contains("model", json);
            Assert.Contains("messages", json);
        }

        [Fact]
        public void Deserialize_InvalidJson_ThrowsException()
        {
            var serializer = new NewtonsoftJsonSerializer();
            Assert.Throws<OpenRouterSerializationException>(() => serializer.Deserialize<ChatCompletionRequest>("not valid json"));
        }
    }
}
