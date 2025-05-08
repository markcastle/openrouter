using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace OpenRouter.Client.SystemTextJson
{
    /// <summary>
    /// Example custom converter for DateTimeOffset to ensure ISO 8601 format.
    /// </summary>
    public class DateTimeOffsetConverter : JsonConverter<DateTimeOffset>
    {
        public override DateTimeOffset Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return DateTimeOffset.Parse(reader.GetString()!);
        }

        public override void Write(Utf8JsonWriter writer, DateTimeOffset value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString("o"));
        }
    }

    // Add additional converters as needed for complex types.
}
