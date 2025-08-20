using SharedKernel;

namespace Domain.Restaurants;

public static class RestaurantErrors
{
    public static Error NotFound(Guid id) =>
        Error.NotFound("Restaurant.NotFound", $"The restaurant with the ID {id} was not found.");

    public static Error NameTooLong =>
        Error.Validation("Restaurant.NameTooLong", "The restaurant name is too long.");

    public static Error DescriptionTooLong =>
        Error.Validation("Restaurant.DescriptionTooLong", "The restaurant description is too long.");

    public static Error InvalidEmail =>
        Error.Validation("Restaurant.InvalidEmail", "The email address is invalid.");

    public static Error InvalidPhoneNumber =>
        Error.Validation("Restaurant.InvalidPhoneNumber", "The phone number is invalid.");
}
