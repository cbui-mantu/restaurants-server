using Application.Abstractions.Messaging;

namespace Application.Restaurants.Get;

public sealed class GetRestaurantsQuery : IQuery<List<RestaurantResponse>>;
