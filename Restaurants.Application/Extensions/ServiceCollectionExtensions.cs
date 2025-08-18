using Microsoft.Extensions.DependencyInjection;

namespace Restaurants.Application.Extensions
{
	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection AddApplication(this IServiceCollection services)
		{
			// Register application-level services (validators, mediators, etc.) here.
			// Intentionally not registering infrastructure implementations to keep dependencies one-way.
			return services;
		}
	}
}


