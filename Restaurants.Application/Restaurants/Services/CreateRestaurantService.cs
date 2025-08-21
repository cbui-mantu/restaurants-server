using Restaurants.Application.Restaurants.Interfaces;
using Restaurants.Application.Restaurants.Mappers;
using Restaurants.Application.Restaurants.Models;

namespace Restaurants.Application.Restaurants.Services
{
    public class CreateRestaurantService : ICreateRestaurantService
    {
        private readonly IRestaurantRepository _repository;
        private readonly IRestaurantMapper _mapper;

        public CreateRestaurantService(IRestaurantRepository repository, IRestaurantMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<int> CreateAsync(CreateRestaurantRequest request, CancellationToken cancellationToken = default)
        {
            var restaurant = _mapper.MapToEntity(request);
            return await _repository.CreateAsync(restaurant, cancellationToken);
        }
    }
}
