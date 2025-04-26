using Domain.Errors;
using Domain.Shared;

namespace Domain.User.ValueObjects;

public sealed record Address 
{
    private Address(string country, string street, string city, string state, string zipCode)
    {
        Country = country;
        Street = street;
        City = city;
        State = state;
        ZipCode = zipCode;
    }

    public string Country { get; }
    public string Street { get; }
    public string City { get; }
    public string State { get; }
    public string ZipCode { get; }

    public static Result<Address> Create(string country, string street, string city, string state, string zipCode)
    {
        if (string.IsNullOrEmpty(country) ||
            string.IsNullOrEmpty(street) ||
            string.IsNullOrEmpty(city) ||
            string.IsNullOrEmpty(state) || 
            string.IsNullOrEmpty(zipCode))
        {
            return Result.Failure<Address>(DomainErrors.AddressErrors.Empty);
        }

        return new Address(country, street, city, state, zipCode);
    }

}



