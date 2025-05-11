using System.Text.Json;
using System.Text.RegularExpressions;

namespace OpenRouter.Client.SystemTextJson
{
    /// <summary>
    /// A JsonNamingPolicy that converts property names to snake_case for System.Text.Json.
    /// </summary>
    /// <summary>
    /// A JsonNamingPolicy that converts property names to snake_case for System.Text.Json.
    /// </summary>
    public class SnakeCaseJsonNamingPolicy : JsonNamingPolicy
    {
        /// <summary>
        /// Regex for converting PascalCase/camelCase to snake_case.
        /// </summary>
        private static readonly Regex _regex = new Regex("([a-z0-9])([A-Z])", RegexOptions.Compiled);

        /// <summary>
        /// Converts a property name to snake_case.
        /// </summary>
        /// <param name="name">The property name to convert.</param>
        /// <returns>The snake_case version of the property name.</returns>
        public override string ConvertName(string name)
        {
            if (string.IsNullOrEmpty(name))
                return name;
            // Convert PascalCase or camelCase to snake_case
            var snake = _regex.Replace(name, "$1_$2").ToLowerInvariant();
            return snake;
        }
    }
}
