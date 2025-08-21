using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Restaurants;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Restaurants.GetById;

internal sealed class GetRestaurantByIdQueryHandler(
    IApplicationDbContext context)
    : IQueryHandler<GetRestaurantByIdQuery, RestaurantResponse>
{
    public async Task<Result<RestaurantResponse>> Handle(GetRestaurantByIdQuery query, CancellationToken cancellationToken)
    {
        var restaurant = await context.Restaurants
            .Include(r => r.Dishes)
            .AsNoTracking()
            .FirstOrDefaultAsync(r => r.Id == query.Id, cancellationToken);

        if (restaurant is null)
        {
            return Result.Failure<RestaurantResponse>(RestaurantErrors.NotFound(query.Id));
        }

        var response = new RestaurantResponse
        {
            Id = restaurant.Id,
            Name = restaurant.Name,
            Description = restaurant.Description,
            Category = restaurant.Category,
            HasDelivery = restaurant.HasDelivery,
            ContactEmail = restaurant.ContactEmail,
            ContactNumber = restaurant.ContactNumber,
            Address = restaurant.Address != null ? new AddressResponse
            {
                City = restaurant.Address.City,
                Street = restaurant.Address.Street,
                PostalCode = restaurant.Address.PostalCode
            } : null,
            Dishes = restaurant.Dishes.Select(d => new DishResponse
            {
                Id = d.Id,
                Name = d.Name,
                Description = d.Description,
                Price = d.Price
            }).ToList()
        };

        return response;
    }
}
