using Microsoft.Extensions.DependencyInjection;
using OpenRouter.Abstractions;

namespace OpenRouter.Client.SystemTextJson
{
    /// <summary>
    /// Extension methods for registering SystemTextJsonSerializer with DI.
    /// </summary>
    public static class SystemTextJsonServiceCollectionExtensions
    {
        /// <summary>
        /// Registers SystemTextJsonSerializer as the ISerializer implementation.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <returns>The service collection.</returns>
        public static IServiceCollection AddSystemTextJsonSerializer(this IServiceCollection services)
        {
            return services.AddSingleton<ISerializer, SystemTextJsonSerializer>();
        }
    }
}
