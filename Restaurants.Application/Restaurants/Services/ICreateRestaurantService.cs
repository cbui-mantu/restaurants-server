using Restaurants.Application.Restaurants.Models;

namespace Restaurants.Application.Restaurants.Services
{
    public interface ICreateRestaurantService
    {
        Task<int> CreateAsync(CreateRestaurantRequest request, CancellationToken cancellationToken = default);
    }
}
