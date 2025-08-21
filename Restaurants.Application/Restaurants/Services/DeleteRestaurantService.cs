using Restaurants.Application.Restaurants.Interfaces;

namespace Restaurants.Application.Restaurants.Services
{
    public class DeleteRestaurantService : IDeleteRestaurantService
    {
        private readonly IRestaurantRepository _repository;

        public DeleteRestaurantService(IRestaurantRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _repository.DeleteAsync(id, cancellationToken);
        }
    }
}
