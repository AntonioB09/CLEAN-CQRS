using Domain.Shared;

namespace Domain.Errors;

public static class DomainErrors
{


    public static class EmailErrors
    {
        public static readonly Error Empty = Error.Validation(
            "Email.Empty", 
            "Email is empty");

        public static readonly Error InvalidFormat = Error.Validation(
            "Email.InvalidFormat", 
            "Email format is invalid");
    }

    public static class FirstNameErrors
    {
        public static readonly Error Empty = Error.Validation(
            "FirstName.Empty",
            "First name is empty.");

        public static readonly Error TooLong = Error.Validation(
            "LastName.TooLong",
            "FirstName name is too long.");
    }

    public static class LastNameErrors
    {
        public static readonly Error Empty = Error.Validation(
            "LastName.Empty",
            "Last name is empty.");

        public static readonly Error TooLong = Error.Validation(
            "LastName.TooLong",
            "Last name is too long.");
    }

    public static class AddressErrors
    {
        public static readonly Error Empty = Error.Validation(
            "Address.Empty",
            "Address is empty.");

        public static readonly Error CountryTooLong = Error.Validation(
            "Address.CountryTooLong",
            "Address field Country is too long.");

        public static readonly Error StreetTooLong = Error.Validation(
            "Address.StreetTooLong",
            "Address field Street is too long.");

        public static readonly Error CityTooLong = Error.Validation(
            "Address.CityTooLong",
            "Address field City is too long.");

        public static readonly Error StateTooLong = Error.Validation(
            "Address.StateTooLong",
            "Address field State is too long.");

        public static readonly Error InvalidZipCodeFormat = Error.Validation(
            "Address.InvalidZipCodeFormat",
            "ZipCode format is invalid.");
    }

    public static class PhoneNumberErrors
    {
        public static readonly Error Empty = Error.Validation(
            "PhoneNumber.Empty",
            "Phone number is empty.");

        public static readonly Error InvalidFormat = Error.Validation(
            "PhoneNumber.InvalidFormat",
            "Phone number format is invalid.");
    }

    public static class UserErrors
    {
        public static Error NotFound(Guid userId) => Error.NotFound(
            "Users.NotFound", $"The user with the Id = '{userId}' was not found");

        public static Error NotFoundByEmail(string email) => Error.NotFound(
            "Users.NotFoundByEmail", $"The user with the Email = '{email}' was not found");

        public static readonly Error EmailNotUnique = Error.Conflict(
            "Users.EmailNotUnique", "The provided email is not unique");
    }

}