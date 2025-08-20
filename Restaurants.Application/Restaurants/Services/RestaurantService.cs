using Restaurants.Application.Restaurants.Interfaces;
using Restaurants.Application.Restaurants.Mappers;
using Restaurants.Application.Restaurants.Models;

namespace Restaurants.Application.Restaurants.Services
{
    public class RestaurantService : IRestaurantService
    {
        private readonly IRestaurantRepository _repository;
        private readonly IRestaurantMapper _mapper;

        public RestaurantService(IRestaurantRepository repository, IRestaurantMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<int> CreateAsync(CreateRestaurantRequest request, CancellationToken cancellationToken = default)
        {
            var restaurant = _mapper.MapToEntity(request);
            return await _repository.CreateAsync(restaurant, cancellationToken);
        }

        public async Task<RestaurantDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var restaurant = await _repository.GetByIdAsync(id, cancellationToken);
            return restaurant is null ? null : _mapper.MapToDto(restaurant);
        }

        public async Task<IReadOnlyList<RestaurantDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var restaurants = await _repository.GetAllAsync(cancellationToken);
            return restaurants.Select(_mapper.MapToDto).ToList();
        }

        public async Task<bool> UpdateAsync(int id, UpdateRestaurantRequest request, CancellationToken cancellationToken = default)
        {
            var restaurant = await _repository.GetByIdAsync(id, cancellationToken);
            if (restaurant is null) return false;

            _mapper.MapToEntity(request, restaurant);
            return await _repository.UpdateAsync(restaurant, cancellationToken);
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _repository.DeleteAsync(id, cancellationToken);
        }
    }
}
