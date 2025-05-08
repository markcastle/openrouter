namespace OpenRouter.Abstractions
{
    /// <summary>
    /// Provides a contract for validating options.
    /// </summary>
    public interface IValidatableOptions
    {
        /// <summary>
        /// Validates the options and throws an exception if invalid.
        /// </summary>
        void Validate();
    }
}
