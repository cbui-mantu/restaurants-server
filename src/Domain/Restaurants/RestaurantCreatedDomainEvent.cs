using SharedKernel;

namespace Domain.Restaurants;

public sealed record RestaurantCreatedDomainEvent(Guid RestaurantId) : IDomainEvent;
