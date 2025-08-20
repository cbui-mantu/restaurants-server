using Restaurants.Application.Restaurants.Models;
using Restaurants.Domain.Entities;

namespace Restaurants.Application.Restaurants.Mappers
{
    public interface IRestaurantMapper
    {
        RestaurantDto MapToDto(Restaurant restaurant);
        Restaurant MapToEntity(CreateRestaurantRequest request);
        void MapToEntity(UpdateRestaurantRequest request, Restaurant restaurant);
    }
}
