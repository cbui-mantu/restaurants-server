namespace Restaurants.Application.Restaurants.Services
{
    public interface IDeleteRestaurantService
    {
        Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
    }
}
