using Microsoft.EntityFrameworkCore;
using Restaurants.Application.Restaurants.Interfaces;
using Restaurants.Domain.Entities;
using Restaurants.Infrastructure.Persistence;

namespace Restaurants.Infrastructure.Restaurants
{
    public class RestaurantRepository : IRestaurantRepository
    {
        private readonly RestaurantsDbContext _dbContext;

        public RestaurantRepository(RestaurantsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> CreateAsync(Restaurant restaurant, CancellationToken cancellationToken = default)
        {
            _dbContext.Restaurants.Add(restaurant);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return restaurant.Id;
        }

        public async Task<Restaurant?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Restaurants
                .AsNoTracking()
                .Include(r => r.Dished)
                .FirstOrDefaultAsync(r => r.Id == id, cancellationToken);
        }

        public async Task<IReadOnlyList<Restaurant>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.Restaurants
                .AsNoTracking()
                .Include(r => r.Dished)
                .ToListAsync(cancellationToken);
        }

        public async Task<bool> UpdateAsync(Restaurant restaurant, CancellationToken cancellationToken = default)
        {
            _dbContext.Restaurants.Update(restaurant);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            var entity = await _dbContext.Restaurants.FirstOrDefaultAsync(r => r.Id == id, cancellationToken);
            if (entity is null) return false;

            _dbContext.Restaurants.Remove(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}


