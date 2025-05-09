using System.Text.Json;
using System.Text.RegularExpressions;

namespace OpenRouter.Client.SystemTextJson
{
    /// <summary>
    /// A JsonNamingPolicy that converts property names to snake_case for System.Text.Json.
    /// </summary>
    public class SnakeCaseJsonNamingPolicy : JsonNamingPolicy
    {
        private static readonly Regex _regex = new Regex("([a-z0-9])([A-Z])", RegexOptions.Compiled);

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
