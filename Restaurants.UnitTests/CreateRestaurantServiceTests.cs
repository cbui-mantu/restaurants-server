using Moq;
using Restaurants.Application.Restaurants.Interfaces;
using Restaurants.Application.Restaurants.Mappers;
using Restaurants.Application.Restaurants.Models;
using Restaurants.Application.Restaurants.Services;
using Restaurants.Domain.Entities;

namespace Restaurants.UnitTests
{
    public class CreateRestaurantServiceTests
    {
        private readonly Mock<IRestaurantRepository> _mockRepository;
        private readonly Mock<IRestaurantMapper> _mockMapper;
        private readonly CreateRestaurantService _service;

        public CreateRestaurantServiceTests()
        {
            _mockRepository = new Mock<IRestaurantRepository>();
            _mockMapper = new Mock<IRestaurantMapper>();
            _service = new CreateRestaurantService(_mockRepository.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task CreateAsync_ShouldReturnRestaurantId()
        {
            // Arrange
            var request = new CreateRestaurantRequest
            {
                Name = "Test Restaurant",
                Description = "Test Description",
                Category = "Italian",
                HasDelivery = true,
                ContactEmail = "test@test.com",
                ContactNumber = "123456789"
            };

            var restaurant = new Restaurant { Id = 1 };
            var expectedId = 1;

            _mockMapper.Setup(m => m.MapToEntity(request)).Returns(restaurant);
            _mockRepository.Setup(r => r.CreateAsync(restaurant, It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedId);

            // Act
            var result = await _service.CreateAsync(request);

            // Assert
            Assert.Equal(expectedId, result);
            _mockMapper.Verify(m => m.MapToEntity(request), Times.Once);
            _mockRepository.Verify(r => r.CreateAsync(restaurant, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task CreateAsync_ShouldHandleNullAddress()
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

            var restaurant = new Restaurant { Id = 1 };
            var expectedId = 1;

            _mockMapper.Setup(m => m.MapToEntity(request)).Returns(restaurant);
            _mockRepository.Setup(r => r.CreateAsync(restaurant, It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedId);

            // Act
            var result = await _service.CreateAsync(request);

            // Assert
            Assert.Equal(expectedId, result);
            _mockMapper.Verify(m => m.MapToEntity(request), Times.Once);
            _mockRepository.Verify(r => r.CreateAsync(restaurant, It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
