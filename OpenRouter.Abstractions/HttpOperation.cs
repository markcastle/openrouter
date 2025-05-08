namespace OpenRouter.Abstractions
{
    /// <summary>
    /// Defines supported HTTP operations.
    /// </summary>
    public enum HttpOperation
    {
        /// <summary>
        /// HTTP GET operation.
        /// </summary>
        Get,
        /// <summary>
        /// HTTP POST operation.
        /// </summary>
        Post,
        /// <summary>
        /// HTTP PUT operation.
        /// </summary>
        Put,
        /// <summary>
        /// HTTP DELETE operation.
        /// </summary>
        Delete,
        /// <summary>
        /// HTTP PATCH operation.
        /// </summary>
        Patch,
        /// <summary>
        /// HTTP HEAD operation.
        /// </summary>
        Head,
        /// <summary>
        /// HTTP OPTIONS operation.
        /// </summary>
        Options
    }
}
