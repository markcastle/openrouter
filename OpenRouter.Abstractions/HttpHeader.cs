namespace OpenRouter.Abstractions
{
    /// <summary>
    /// Represents an HTTP header (name-value pair).
    /// </summary>
    public class HttpHeader
    {
        /// <summary>
        /// Gets or sets the header name.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the header value.
        /// </summary>
        public string Value { get; set; } = string.Empty;

        /// <summary>
        /// Initializes a new instance of <see cref="HttpHeader"/>.
        /// </summary>
        public HttpHeader() { }

        /// <summary>
        /// Initializes a new instance of <see cref="HttpHeader"/> with name and value.
        /// </summary>
        public HttpHeader(string name, string value)
        {
            Name = name;
            Value = value;
        }
    }
}
