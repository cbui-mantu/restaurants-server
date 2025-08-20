using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Restaurants.Get;

internal sealed class GetRestaurantsQueryHandler(
    IApplicationDbContext context)
    : IQueryHandler<GetRestaurantsQuery, List<RestaurantResponse>>
{
    public async Task<Result<List<RestaurantResponse>>> Handle(GetRestaurantsQuery query, CancellationToken cancellationToken)
    {
        var restaurants = await context.Restaurants
            .Include(r => r.Dishes)
            .AsNoTracking()
            .Select(r => new RestaurantResponse
            {
                Id = r.Id,
                Name = r.Name,
                Description = r.Description,
                Category = r.Category,
                HasDelivery = r.HasDelivery,
                ContactEmail = r.ContactEmail,
                ContactNumber = r.ContactNumber,
                Address = r.Address != null ? new AddressResponse
                {
                    City = r.Address.City,
                    Street = r.Address.Street,
                    PostalCode = r.Address.PostalCode
                } : null,
                Dishes = r.Dishes.Select(d => new DishResponse
                {
                    Id = d.Id,
                    Name = d.Name,
                    Description = d.Description,
                    Price = d.Price
                }).ToList()
            })
            .ToListAsync(cancellationToken);

        return restaurants;
    }
}
