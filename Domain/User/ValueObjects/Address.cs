using Domain.Errors;
using Domain.Shared;
using System.Text.RegularExpressions;

namespace Domain.User.ValueObjects;

public sealed record Address 
{
    public const int MaxLength = 100;
    private static readonly Regex ZipCodeRegex = new Regex(@"^[0-9]{5}$");

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

        if (country.Length > MaxLength)
        {
            return Result.Failure<Address>(DomainErrors.AddressErrors.CountryTooLong);
        }

        if (street.Length > MaxLength)
        {
            return Result.Failure<Address>(DomainErrors.AddressErrors.StreetTooLong);
        }

        if (city.Length > MaxLength)
        {
            return Result.Failure<Address>(DomainErrors.AddressErrors.CityTooLong);
        }

        if (state.Length > MaxLength)
        {
            return Result.Failure<Address>(DomainErrors.AddressErrors.StateTooLong);
        }

        if (!ZipCodeRegex.IsMatch(zipCode))
        {
            return Result.Failure<Address>(DomainErrors.AddressErrors.InvalidZipCodeFormat);
        }
        
        return new Address(country, street, city, state, zipCode);
    }

}



