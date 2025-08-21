using Microsoft.Extensions.DependencyInjection;
using Restaurants.Application.Restaurants.Mappers;
using Restaurants.Application.Restaurants.Services;

namespace Restaurants.Application.Extensions
{
	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection AddApplication(this IServiceCollection services)
		{
			// Register application-level services
			services.AddScoped<IRestaurantMapper, RestaurantMapper>();
			services.AddScoped<ICreateRestaurantService, CreateRestaurantService>();
			services.AddScoped<IGetRestaurantService, GetRestaurantService>();
			services.AddScoped<IUpdateRestaurantService, UpdateRestaurantService>();
			services.AddScoped<IDeleteRestaurantService, DeleteRestaurantService>();
			
			return services;
		}
	}
}


