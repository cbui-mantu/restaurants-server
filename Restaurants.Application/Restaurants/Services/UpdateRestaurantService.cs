using Restaurants.Application.Restaurants.Interfaces;
using Restaurants.Application.Restaurants.Mappers;
using Restaurants.Application.Restaurants.Models;

namespace Restaurants.Application.Restaurants.Services
{
    public class UpdateRestaurantService : IUpdateRestaurantService
    {
        private readonly IRestaurantRepository _repository;
        private readonly IRestaurantMapper _mapper;

        public UpdateRestaurantService(IRestaurantRepository repository, IRestaurantMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<bool> UpdateAsync(int id, UpdateRestaurantRequest request, CancellationToken cancellationToken = default)
        {
            var restaurant = await _repository.GetByIdAsync(id, cancellationToken);
            if (restaurant is null) return false;

            _mapper.MapToEntity(request, restaurant);
            return await _repository.UpdateAsync(restaurant, cancellationToken);
        }
    }
}
