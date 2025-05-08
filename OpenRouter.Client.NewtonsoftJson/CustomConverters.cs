using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace OpenRouter.Client.NewtonsoftJson
{
    /// <summary>
    /// Example custom converter for DateTimeOffset, demonstrating how to implement converters for complex types.
    /// </summary>
    public class DateTimeOffsetConverter : JsonConverter<DateTimeOffset>
    {
        public override void WriteJson(JsonWriter writer, DateTimeOffset value, JsonSerializer serializer)
        {
            writer.WriteValue(value.ToString("o"));
        }

        public override DateTimeOffset ReadJson(JsonReader reader, Type objectType, DateTimeOffset existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var str = reader.Value?.ToString();
            return str != null ? DateTimeOffset.Parse(str) : default;
        }
    }
}
