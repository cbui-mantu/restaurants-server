using Restaurants.Application.Restaurants.Interfaces;
using Restaurants.Application.Restaurants.Mappers;
using Restaurants.Application.Restaurants.Models;

namespace Restaurants.Application.Restaurants.Services
{
    public class GetRestaurantService : IGetRestaurantService
    {
        private readonly IRestaurantRepository _repository;
        private readonly IRestaurantMapper _mapper;

        public GetRestaurantService(IRestaurantRepository repository, IRestaurantMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
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
    }
}
