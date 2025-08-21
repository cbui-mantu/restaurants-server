using Moq;
using Restaurants.Application.Restaurants.Interfaces;
using Restaurants.Application.Restaurants.Services;

namespace Restaurants.UnitTests
{
    public class DeleteRestaurantServiceTests
    {
        private readonly Mock<IRestaurantRepository> _mockRepository;
        private readonly DeleteRestaurantService _service;

        public DeleteRestaurantServiceTests()
        {
            _mockRepository = new Mock<IRestaurantRepository>();
            _service = new DeleteRestaurantService(_mockRepository.Object);
        }

        [Fact]
        public async Task DeleteAsync_WhenRestaurantExists_ShouldReturnTrue()
        {
            // Arrange
            var id = 1;
            _mockRepository.Setup(r => r.DeleteAsync(id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            // Act
            var result = await _service.DeleteAsync(id);

            // Assert
            Assert.True(result);
            _mockRepository.Verify(r => r.DeleteAsync(id, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_WhenRestaurantDoesNotExist_ShouldReturnFalse()
        {
            // Arrange
            var id = 1;
            _mockRepository.Setup(r => r.DeleteAsync(id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            // Act
            var result = await _service.DeleteAsync(id);

            // Assert
            Assert.False(result);
            _mockRepository.Verify(r => r.DeleteAsync(id, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ShouldHandleMultipleIds()
        {
            // Arrange
            var id1 = 1;
            var id2 = 2;
            _mockRepository.Setup(r => r.DeleteAsync(id1, It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);
            _mockRepository.Setup(r => r.DeleteAsync(id2, It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            // Act
            var result1 = await _service.DeleteAsync(id1);
            var result2 = await _service.DeleteAsync(id2);

            // Assert
            Assert.True(result1);
            Assert.False(result2);
            _mockRepository.Verify(r => r.DeleteAsync(id1, It.IsAny<CancellationToken>()), Times.Once);
            _mockRepository.Verify(r => r.DeleteAsync(id2, It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
