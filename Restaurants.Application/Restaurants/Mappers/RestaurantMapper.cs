using Restaurants.Application.Restaurants.Models;
using Restaurants.Domain.Entities;

namespace Restaurants.Application.Restaurants.Mappers
{
    public class RestaurantMapper : IRestaurantMapper
    {
        public RestaurantDto MapToDto(Restaurant restaurant)
        {
            return new RestaurantDto
            {
                Id = restaurant.Id,
                Name = restaurant.Name,
                Description = restaurant.Description,
                Category = restaurant.Category,
                HasDelivery = restaurant.HasDelivery,
                ContactEmail = restaurant.ContactEmail,
                ContactNumber = restaurant.ContactNumber,
                Address = restaurant.Address is null ? null : new AddressDto
                {
                    City = restaurant.Address.City,
                    Street = restaurant.Address.Street,
                    PostalCode = restaurant.Address.PostalCode
                },
                Dishes = restaurant.Dished.Select(d => new DishDto
                {
                    Id = d.Id,
                    Name = d.Name,
                    Description = d.Description,
                    Price = d.Price
                }).ToList()
            };
        }

        public Restaurant MapToEntity(CreateRestaurantRequest request)
        {
            return new Restaurant
            {
                Name = request.Name,
                Description = request.Description,
                Category = request.Category,
                HasDelivery = request.HasDelivery,
                ContactEmail = request.ContactEmail,
                ContactNumber = request.ContactNumber,
                Address = request.Address is null ? null : new Address
                {
                    City = request.Address.City,
                    Street = request.Address.Street,
                    PostalCode = request.Address.PostalCode
                }
            };
        }

        public void MapToEntity(UpdateRestaurantRequest request, Restaurant restaurant)
        {
            restaurant.Name = request.Name;
            restaurant.Description = request.Description;
            restaurant.Category = request.Category;
            restaurant.HasDelivery = request.HasDelivery;
            restaurant.ContactEmail = request.ContactEmail;
            restaurant.ContactNumber = request.ContactNumber;
            restaurant.Address = request.Address is null ? null : new Address
            {
                City = request.Address.City,
                Street = request.Address.Street,
                PostalCode = request.Address.PostalCode
            };
        }
    }
}
