using SharedKernel;

namespace Domain.Restaurants;

public sealed class Restaurant : Entity
{
    public Restaurant(
        Guid id,
        string name,
        string description,
        string category,
        bool hasDelivery,
        string? contactEmail,
        string? contactNumber,
        Address? address)
    {
        Id = id;
        Name = name;
        Description = description;
        Category = category;
        HasDelivery = hasDelivery;
        ContactEmail = contactEmail;
        ContactNumber = contactNumber;
        Address = address;
        Dishes = [];
    }

    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public string Category { get; private set; }
    public bool HasDelivery { get; private set; }
    public string? ContactEmail { get; private set; }
    public string? ContactNumber { get; private set; }
    public Address? Address { get; private set; }
    public List<Dish> Dishes { get; private set; }

    public void UpdateDetails(
        string name,
        string description,
        string category,
        bool hasDelivery,
        string? contactEmail,
        string? contactNumber,
        Address? address)
    {
        Name = name;
        Description = description;
        Category = category;
        HasDelivery = hasDelivery;
        ContactEmail = contactEmail;
        ContactNumber = contactNumber;
        Address = address;
    }

    public void AddDish(Dish dish)
    {
        Dishes.Add(dish);
    }

    public void RemoveDish(Guid dishId)
    {
        var dish = Dishes.FirstOrDefault(d => d.Id == dishId);
        if (dish != null)
        {
            Dishes.Remove(dish);
        }
    }
}
