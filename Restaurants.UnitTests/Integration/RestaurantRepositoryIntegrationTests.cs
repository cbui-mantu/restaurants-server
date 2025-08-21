using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Restaurants.Application.Restaurants.Interfaces;
using Restaurants.Domain.Entities;
using Restaurants.Infrastructure.Persistence;
using Restaurants.Infrastructure.Restaurants;

namespace Restaurants.UnitTests.Integration
{
    public class RestaurantRepositoryIntegrationTests : IDisposable
    {
        private readonly RestaurantsDbContext _context;
        private readonly IRestaurantRepository _repository;

        public RestaurantRepositoryIntegrationTests()
        {
            var services = new ServiceCollection();
            services.AddDbContext<RestaurantsDbContext>(options =>
                options.UseInMemoryDatabase("TestDatabase_" + Guid.NewGuid()));

            var serviceProvider = services.BuildServiceProvider();
            _context = serviceProvider.GetRequiredService<RestaurantsDbContext>();
            _repository = new RestaurantRepository(_context);
        }

        [Fact]
        public async Task CreateAsync_ShouldCreateRestaurant()
        {
            // Arrange
            var restaurant = new Restaurant
            {
                Name = "Test Restaurant",
                Description = "Test Description",
                Category = "Italian",
                HasDelivery = true,
                ContactEmail = "test@test.com",
                ContactNumber = "123456789"
            };

            // Act
            var result = await _repository.CreateAsync(restaurant);

            // Assert
            Assert.True(result > 0);
            var createdRestaurant = await _context.Restaurants.FindAsync(result);
            Assert.NotNull(createdRestaurant);
            Assert.Equal(restaurant.Name, createdRestaurant.Name);
        }

        [Fact]
        public async Task GetByIdAsync_WhenRestaurantExists_ShouldReturnRestaurant()
        {
            // Arrange
            var restaurant = new Restaurant
            {
                Name = "Test Restaurant",
                Description = "Test Description",
                Category = "Italian",
                HasDelivery = true,
                ContactEmail = "test@test.com",
                ContactNumber = "123456789"
            };

            _context.Restaurants.Add(restaurant);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetByIdAsync(restaurant.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(restaurant.Name, result.Name);
            Assert.Equal(restaurant.Description, result.Description);
        }

        [Fact]
        public async Task GetByIdAsync_WhenRestaurantDoesNotExist_ShouldReturnNull()
        {
            // Act
            var result = await _repository.GetByIdAsync(999);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllRestaurants()
        {
            // Arrange
            var restaurants = new List<Restaurant>
            {
                new Restaurant { Name = "Restaurant 1", Description = "Description 1", Category = "Italian" },
                new Restaurant { Name = "Restaurant 2", Description = "Description 2", Category = "Mexican" },
                new Restaurant { Name = "Restaurant 3", Description = "Description 3", Category = "Chinese" }
            };

            _context.Restaurants.AddRange(restaurants);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(3, result.Count);
            Assert.Contains(result, r => r.Name == "Restaurant 1");
            Assert.Contains(result, r => r.Name == "Restaurant 2");
            Assert.Contains(result, r => r.Name == "Restaurant 3");
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateRestaurant()
        {
            // Arrange
            var restaurant = new Restaurant
            {
                Name = "Original Name",
                Description = "Original Description",
                Category = "Italian",
                HasDelivery = true,
                ContactEmail = "original@test.com",
                ContactNumber = "123456789"
            };

            _context.Restaurants.Add(restaurant);
            await _context.SaveChangesAsync();

            restaurant.Name = "Updated Name";
            restaurant.Description = "Updated Description";

            // Act
            var result = await _repository.UpdateAsync(restaurant);

            // Assert
            Assert.True(result);
            var updatedRestaurant = await _context.Restaurants.FindAsync(restaurant.Id);
            Assert.NotNull(updatedRestaurant);
            Assert.Equal("Updated Name", updatedRestaurant.Name);
            Assert.Equal("Updated Description", updatedRestaurant.Description);
        }

        [Fact]
        public async Task DeleteAsync_WhenRestaurantExists_ShouldDeleteRestaurant()
        {
            // Arrange
            var restaurant = new Restaurant
            {
                Name = "Test Restaurant",
                Description = "Test Description",
                Category = "Italian",
                HasDelivery = true,
                ContactEmail = "test@test.com",
                ContactNumber = "123456789"
            };

            _context.Restaurants.Add(restaurant);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.DeleteAsync(restaurant.Id);

            // Assert
            Assert.True(result);
            var deletedRestaurant = await _context.Restaurants.FindAsync(restaurant.Id);
            Assert.Null(deletedRestaurant);
        }

        [Fact]
        public async Task DeleteAsync_WhenRestaurantDoesNotExist_ShouldReturnFalse()
        {
            // Act
            var result = await _repository.DeleteAsync(999);

            // Assert
            Assert.False(result);
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
