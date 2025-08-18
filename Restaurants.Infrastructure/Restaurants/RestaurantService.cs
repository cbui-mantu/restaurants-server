using Microsoft.EntityFrameworkCore;
using Restaurants.Application.Restaurants.Interfaces;
using Restaurants.Application.Restaurants.Models;
using Restaurants.Domain.Entities;
using Restaurants.Infrastructure.Persistence;

namespace Restaurants.Infrastructure.Restaurants
{
	internal class RestaurantService(RestaurantsDbContext dbContext) : IRestaurantService
	{
		public async Task<int> CreateAsync(CreateRestaurantRequest request, CancellationToken cancellationToken = default)
		{
			var entity = new Restaurant
			{
				Name = request.Name,
				Description = request.Description,
				Category = request.Category,
				HasDelivery = request.HasDelivery,
				ContactEmail = request.ContactEmail,
				ContactNumber = request.ContactNumber,
				Address = request.Address is null ? null : new Address
				{
					City = request.Address.City,
					Street = request.Address.Street,
					PostalCode = request.Address.PostalCode
				}
			};
			dbContext.Restaurants.Add(entity);
			await dbContext.SaveChangesAsync(cancellationToken);
			return entity.Id;
		}

		public async Task<RestaurantDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
		{
			var entity = await dbContext.Restaurants.AsNoTracking().Include(r => r.Dished).FirstOrDefaultAsync(r => r.Id == id, cancellationToken);
			return entity is null ? null : MapToDto(entity);
		}

		public async Task<IReadOnlyList<RestaurantDto>> GetAllAsync(CancellationToken cancellationToken = default)
		{
			var entities = await dbContext.Restaurants.AsNoTracking().Include(r => r.Dished).ToListAsync(cancellationToken);
			return entities.Select(MapToDto).ToList();
		}

		public async Task<bool> UpdateAsync(int id, UpdateRestaurantRequest request, CancellationToken cancellationToken = default)
		{
			var entity = await dbContext.Restaurants.FirstOrDefaultAsync(r => r.Id == id, cancellationToken);
			if (entity is null) return false;
			entity.Name = request.Name;
			entity.Description = request.Description;
			entity.Category = request.Category;
			entity.HasDelivery = request.HasDelivery;
			entity.ContactEmail = request.ContactEmail;
			entity.ContactNumber = request.ContactNumber;
			entity.Address = request.Address is null ? null : new Address
			{
				City = request.Address.City,
				Street = request.Address.Street,
				PostalCode = request.Address.PostalCode
			};
			await dbContext.SaveChangesAsync(cancellationToken);
			return true;
		}

		public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
		{
			var entity = await dbContext.Restaurants.FirstOrDefaultAsync(r => r.Id == id, cancellationToken);
			if (entity is null) return false;
			dbContext.Restaurants.Remove(entity);
			await dbContext.SaveChangesAsync(cancellationToken);
			return true;
		}

		private static RestaurantDto MapToDto(Restaurant entity)
		{
			return new RestaurantDto
			{
				Id = entity.Id,
				Name = entity.Name,
				Description = entity.Description,
				Category = entity.Category,
				HasDelivery = entity.HasDelivery,
				ContactEmail = entity.ContactEmail,
				ContactNumber = entity.ContactNumber,
				Address = entity.Address is null ? null : new AddressDto
				{
					City = entity.Address.City,
					Street = entity.Address.Street,
					PostalCode = entity.Address.PostalCode
				},
				Dishes = entity.Dished.Select(d => new DishDto
				{
					Id = d.Id,
					Name = d.Name,
					Description = d.Description,
					Price = d.Price
				}).ToList()
			};
		}
	}
}


