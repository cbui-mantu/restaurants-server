using Restaurants.Application.Restaurants.Models;

namespace Restaurants.Application.Restaurants.Services
{
    public interface IUpdateRestaurantService
    {
        Task<bool> UpdateAsync(int id, UpdateRestaurantRequest request, CancellationToken cancellationToken = default);
    }
}
