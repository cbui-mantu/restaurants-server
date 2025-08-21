using Moq;
using Restaurants.Application.Restaurants.Interfaces;
using Restaurants.Application.Restaurants.Mappers;
using Restaurants.Application.Restaurants.Models;
using Restaurants.Application.Restaurants.Services;
using Restaurants.Domain.Entities;

namespace Restaurants.UnitTests
{
    public class GetRestaurantServiceTests
    {
        private readonly Mock<IRestaurantRepository> _mockRepository;
        private readonly Mock<IRestaurantMapper> _mockMapper;
        private readonly GetRestaurantService _service;

        public GetRestaurantServiceTests()
        {
            _mockRepository = new Mock<IRestaurantRepository>();
            _mockMapper = new Mock<IRestaurantMapper>();
            _service = new GetRestaurantService(_mockRepository.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task GetByIdAsync_WhenRestaurantExists_ShouldReturnRestaurantDto()
        {
            // Arrange
            var id = 1;
            var restaurant = new Restaurant { Id = id, Name = "Test Restaurant" };
            var expectedDto = new RestaurantDto { Id = id, Name = "Test Restaurant" };

            _mockRepository.Setup(r => r.GetByIdAsync(id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(restaurant);
            _mockMapper.Setup(m => m.MapToDto(restaurant)).Returns(expectedDto);

            // Act
            var result = await _service.GetByIdAsync(id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedDto.Id, result.Id);
            Assert.Equal(expectedDto.Name, result.Name);
            _mockRepository.Verify(r => r.GetByIdAsync(id, It.IsAny<CancellationToken>()), Times.Once);
            _mockMapper.Verify(m => m.MapToDto(restaurant), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_WhenRestaurantDoesNotExist_ShouldReturnNull()
        {
            // Arrange
            var id = 1;
            Restaurant? restaurant = null;

            _mockRepository.Setup(r => r.GetByIdAsync(id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(restaurant);

            // Act
            var result = await _service.GetByIdAsync(id);

            // Assert
            Assert.Null(result);
            _mockRepository.Verify(r => r.GetByIdAsync(id, It.IsAny<CancellationToken>()), Times.Once);
            _mockMapper.Verify(m => m.MapToDto(It.IsAny<Restaurant>()), Times.Never);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnListOfRestaurantDtos()
        {
            // Arrange
            var restaurants = new List<Restaurant>
            {
                new Restaurant { Id = 1, Name = "Restaurant 1" },
                new Restaurant { Id = 2, Name = "Restaurant 2" }
            };

            var expectedDtos = new List<RestaurantDto>
            {
                new RestaurantDto { Id = 1, Name = "Restaurant 1" },
                new RestaurantDto { Id = 2, Name = "Restaurant 2" }
            };

            _mockRepository.Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(restaurants);
            _mockMapper.Setup(m => m.MapToDto(restaurants[0])).Returns(expectedDtos[0]);
            _mockMapper.Setup(m => m.MapToDto(restaurants[1])).Returns(expectedDtos[1]);

            // Act
            var result = await _service.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Equal(expectedDtos[0].Id, result[0].Id);
            Assert.Equal(expectedDtos[1].Id, result[1].Id);
            _mockRepository.Verify(r => r.GetAllAsync(It.IsAny<CancellationToken>()), Times.Once);
            _mockMapper.Verify(m => m.MapToDto(It.IsAny<Restaurant>()), Times.Exactly(2));
        }

        [Fact]
        public async Task GetAllAsync_WhenNoRestaurants_ShouldReturnEmptyList()
        {
            // Arrange
            var restaurants = new List<Restaurant>();

            _mockRepository.Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(restaurants);

            // Act
            var result = await _service.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
            _mockRepository.Verify(r => r.GetAllAsync(It.IsAny<CancellationToken>()), Times.Once);
            _mockMapper.Verify(m => m.MapToDto(It.IsAny<Restaurant>()), Times.Never);
        }
    }
}
