using Restaurants.Application.Restaurants.Mappers;
using Restaurants.Application.Restaurants.Models;
using Restaurants.Domain.Entities;

namespace Restaurants.UnitTests
{
    public class RestaurantMapperTests
    {
        private readonly IRestaurantMapper _mapper;

        public RestaurantMapperTests()
        {
            _mapper = new RestaurantMapper();
        }

        [Fact]
        public void MapToDto_ShouldMapRestaurantToDto()
        {
            // Arrange
            var restaurant = new Restaurant
            {
                Id = 1,
                Name = "Test Restaurant",
                Description = "Test Description",
                Category = "Italian",
                HasDelivery = true,
                ContactEmail = "test@test.com",
                ContactNumber = "123456789",
                Address = new Address
                {
                    City = "Test City",
                    Street = "Test Street",
                    PostalCode = "12345"
                },
                Dished = new List<Dish>
                {
                    new Dish
                    {
                        Id = 1,
                        Name = "Test Dish",
                        Description = "Test Dish Description",
                        Price = 10.99m
                    }
                }
            };

            // Act
            var result = _mapper.MapToDto(restaurant);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(restaurant.Id, result.Id);
            Assert.Equal(restaurant.Name, result.Name);
            Assert.Equal(restaurant.Description, result.Description);
            Assert.Equal(restaurant.Category, result.Category);
            Assert.Equal(restaurant.HasDelivery, result.HasDelivery);
            Assert.Equal(restaurant.ContactEmail, result.ContactEmail);
            Assert.Equal(restaurant.ContactNumber, result.ContactNumber);
            Assert.NotNull(result.Address);
            Assert.Equal(restaurant.Address!.City, result.Address.City);
            Assert.Equal(restaurant.Address.Street, result.Address.Street);
            Assert.Equal(restaurant.Address.PostalCode, result.Address.PostalCode);
            Assert.Single(result.Dishes);
            Assert.Equal(restaurant.Dished.First().Id, result.Dishes.First().Id);
            Assert.Equal(restaurant.Dished.First().Name, result.Dishes.First().Name);
        }

        [Fact]
        public void MapToDto_WithNullAddress_ShouldMapCorrectly()
        {
            // Arrange
            var restaurant = new Restaurant
            {
                Id = 1,
                Name = "Test Restaurant",
                Description = "Test Description",
                Category = "Italian",
                HasDelivery = true,
                ContactEmail = "test@test.com",
                ContactNumber = "123456789",
                Address = null,
                Dished = new List<Dish>()
            };

            // Act
            var result = _mapper.MapToDto(restaurant);

            // Assert
            Assert.NotNull(result);
            Assert.Null(result.Address);
            Assert.Empty(result.Dishes);
        }

        [Fact]
        public void MapToEntity_ShouldMapCreateRequestToRestaurant()
        {
            // Arrange
            var request = new CreateRestaurantRequest
            {
                Name = "Test Restaurant",
                Description = "Test Description",
                Category = "Italian",
                HasDelivery = true,
                ContactEmail = "test@test.com",
                ContactNumber = "123456789",
                Address = new AddressDto
                {
                    City = "Test City",
                    Street = "Test Street",
                    PostalCode = "12345"
                }
            };

            // Act
            var result = _mapper.MapToEntity(request);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(request.Name, result.Name);
            Assert.Equal(request.Description, result.Description);
            Assert.Equal(request.Category, result.Category);
            Assert.Equal(request.HasDelivery, result.HasDelivery);
            Assert.Equal(request.ContactEmail, result.ContactEmail);
            Assert.Equal(request.ContactNumber, result.ContactNumber);
            Assert.NotNull(result.Address);
            Assert.Equal(request.Address!.City, result.Address.City);
            Assert.Equal(request.Address.Street, result.Address.Street);
            Assert.Equal(request.Address.PostalCode, result.Address.PostalCode);
        }

        [Fact]
        public void MapToEntity_WithNullAddress_ShouldMapCorrectly()
        {
            // Arrange
            var request = new CreateRestaurantRequest
            {
                Name = "Test Restaurant",
                Description = "Test Description",
                Category = "Italian",
                HasDelivery = true,
                ContactEmail = "test@test.com",
                ContactNumber = "123456789",
                Address = null
            };

            // Act
            var result = _mapper.MapToEntity(request);

            // Assert
            Assert.NotNull(result);
            Assert.Null(result.Address);
        }
    }
}