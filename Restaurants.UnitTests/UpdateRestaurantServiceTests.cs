using Moq;
using Restaurants.Application.Restaurants.Interfaces;
using Restaurants.Application.Restaurants.Mappers;
using Restaurants.Application.Restaurants.Models;
using Restaurants.Application.Restaurants.Services;
using Restaurants.Domain.Entities;

namespace Restaurants.UnitTests
{
    public class UpdateRestaurantServiceTests
    {
        private readonly Mock<IRestaurantRepository> _mockRepository;
        private readonly Mock<IRestaurantMapper> _mockMapper;
        private readonly UpdateRestaurantService _service;

        public UpdateRestaurantServiceTests()
        {
            _mockRepository = new Mock<IRestaurantRepository>();
            _mockMapper = new Mock<IRestaurantMapper>();
            _service = new UpdateRestaurantService(_mockRepository.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task UpdateAsync_WhenRestaurantExists_ShouldReturnTrue()
        {
            // Arrange
            var id = 1;
            var request = new UpdateRestaurantRequest
            {
                Name = "Updated Restaurant",
                Description = "Updated Description",
                Category = "Italian",
                HasDelivery = true,
                ContactEmail = "updated@test.com",
                ContactNumber = "987654321"
            };

            var restaurant = new Restaurant { Id = id, Name = "Original Name" };

            _mockRepository.Setup(r => r.GetByIdAsync(id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(restaurant);
            _mockRepository.Setup(r => r.UpdateAsync(restaurant, It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            // Act
            var result = await _service.UpdateAsync(id, request);

            // Assert
            Assert.True(result);
            _mockRepository.Verify(r => r.GetByIdAsync(id, It.IsAny<CancellationToken>()), Times.Once);
            _mockMapper.Verify(m => m.MapToEntity(request, restaurant), Times.Once);
            _mockRepository.Verify(r => r.UpdateAsync(restaurant, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_WhenRestaurantDoesNotExist_ShouldReturnFalse()
        {
            // Arrange
            var id = 1;
            var request = new UpdateRestaurantRequest
            {
                Name = "Updated Restaurant",
                Description = "Updated Description",
                Category = "Italian",
                HasDelivery = true,
                ContactEmail = "updated@test.com",
                ContactNumber = "987654321"
            };

            Restaurant? restaurant = null;

            _mockRepository.Setup(r => r.GetByIdAsync(id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(restaurant);

            // Act
            var result = await _service.UpdateAsync(id, request);

            // Assert
            Assert.False(result);
            _mockRepository.Verify(r => r.GetByIdAsync(id, It.IsAny<CancellationToken>()), Times.Once);
            _mockMapper.Verify(m => m.MapToEntity(It.IsAny<UpdateRestaurantRequest>(), It.IsAny<Restaurant>()), Times.Never);
            _mockRepository.Verify(r => r.UpdateAsync(It.IsAny<Restaurant>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public async Task UpdateAsync_ShouldHandleNullAddress()
        {
            // Arrange
            var id = 1;
            var request = new UpdateRestaurantRequest
            {
                Name = "Updated Restaurant",
                Description = "Updated Description",
                Category = "Italian",
                HasDelivery = true,
                ContactEmail = "updated@test.com",
                ContactNumber = "987654321",
                Address = null
            };

            var restaurant = new Restaurant { Id = id, Name = "Original Name" };

            _mockRepository.Setup(r => r.GetByIdAsync(id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(restaurant);
            _mockRepository.Setup(r => r.UpdateAsync(restaurant, It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            // Act
            var result = await _service.UpdateAsync(id, request);

            // Assert
            Assert.True(result);
            _mockRepository.Verify(r => r.GetByIdAsync(id, It.IsAny<CancellationToken>()), Times.Once);
            _mockMapper.Verify(m => m.MapToEntity(request, restaurant), Times.Once);
            _mockRepository.Verify(r => r.UpdateAsync(restaurant, It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
