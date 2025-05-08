namespace OpenRouter.Abstractions
{
    /// <summary>
    /// Represents an HTTP query or body parameter (name-value pair).
    /// </summary>
    public class HttpParameter
    {
        /// <summary>
        /// Gets or sets the parameter name.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the parameter value.
        /// </summary>
        public string Value { get; set; } = string.Empty;

        /// <summary>
        /// Initializes a new instance of <see cref="HttpParameter"/>.
        /// </summary>
        public HttpParameter() { }

        /// <summary>
        /// Initializes a new instance of <see cref="HttpParameter"/> with name and value.
        /// </summary>
        public HttpParameter(string name, string value)
        {
            Name = name;
            Value = value;
        }
    }
}
