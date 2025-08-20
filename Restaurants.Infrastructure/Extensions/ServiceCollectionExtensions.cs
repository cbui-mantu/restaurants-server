using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Restaurants.Infrastructure.Persistence;
using Restaurants.Infrastructure.Seeders;
using Restaurants.Application.Restaurants.Interfaces;
using Restaurants.Infrastructure.Restaurants;
using Restaurants.Application.Auth.Interfaces;
using Restaurants.Infrastructure.Auth;

namespace Restaurants.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("RestaurantsDb");
            services.AddDbContext<RestaurantsDbContext>(opt => opt.UseSqlServer(connectionString));

            services.AddScoped<IRestaurantSeeder, RestaurantSeeder>();
            services.AddScoped<IRestaurantService, RestaurantService>();
            services.AddScoped<IUserService, InMemoryUserService>();
        }
    }
}
