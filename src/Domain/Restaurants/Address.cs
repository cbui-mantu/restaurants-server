namespace Domain.Restaurants;

public sealed class Address
{
    public Address(string city, string street, string postalCode)
    {
        City = city;
        Street = street;
        PostalCode = postalCode;
    }

    public string City { get; private set; }
    public string Street { get; private set; }
    public string PostalCode { get; private set; }
}
