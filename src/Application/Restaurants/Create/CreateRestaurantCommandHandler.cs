using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Restaurants;
using SharedKernel;

namespace Application.Restaurants.Create;

internal sealed class CreateRestaurantCommandHandler(
    IApplicationDbContext context,
    IDateTimeProvider dateTimeProvider)
    : ICommandHandler<CreateRestaurantCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateRestaurantCommand command, CancellationToken cancellationToken)
    {
        var address = command.Address != null 
            ? new Address(command.Address.City, command.Address.Street, command.Address.PostalCode)
            : null;

        var restaurant = new Restaurant(
            Guid.NewGuid(),
            command.Name,
            command.Description,
            command.Category,
            command.HasDelivery,
            command.ContactEmail,
            command.ContactNumber,
            address);

        restaurant.Raise(new RestaurantCreatedDomainEvent(restaurant.Id));

        context.Restaurants.Add(restaurant);

        await context.SaveChangesAsync(cancellationToken);

        return restaurant.Id;
    }
}
