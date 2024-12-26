using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Bag_E_Commerce.Extensions
{
    public static class ServiceRegistrationExtensions
    {
        /// <summary>
        /// Automatically registers services by matching classes with their corresponding interfaces
        /// based on naming conventions (e.g., IBagService -> BagService).
        /// </summary>
        /// <param name="services">The IServiceCollection to add services to.</param>
        /// <param name="assembly">The assembly to scan for services and interfaces.</param>
        /// <returns>The updated IServiceCollection.</returns>
        public static IServiceCollection RegisterServices(this IServiceCollection services, Assembly assembly)
        {
            // Get all types in the specified assembly
            var types = assembly.GetTypes();

            // Find interfaces and their corresponding implementations
            var servicePairs = types
                .Where(type => type.IsClass && !type.IsAbstract) // Only non-abstract classes
                .Select(type => new
                {
                    Implementation = type,
                    Interface = type.GetInterfaces().FirstOrDefault(i =>
                        i.Name == $"I{type.Name}" && i.Namespace != null && i.Namespace.Contains("Interfaces"))
                })
                .Where(pair => pair.Interface != null); // Only pairs with matching interfaces

            // Register each service pair in the DI container
            foreach (var pair in servicePairs)
            {
                services.AddScoped(pair.Interface, pair.Implementation);
            }

            return services;
        }
    }
}
