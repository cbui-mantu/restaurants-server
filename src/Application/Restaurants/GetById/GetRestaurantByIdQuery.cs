using Application.Abstractions.Messaging;

namespace Application.Restaurants.GetById;

public sealed class GetRestaurantByIdQuery : IQuery<RestaurantResponse>
{
    public Guid Id { get; set; }
}
