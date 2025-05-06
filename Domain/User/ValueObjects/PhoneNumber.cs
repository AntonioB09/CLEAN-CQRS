using Domain.Errors;
using Domain.Shared;
using System.Text.RegularExpressions;
using System.Text.Json.Serialization;


namespace Domain.User.ValueObjects;

public sealed partial record PhoneNumber 
{
    private const int DefaultLenght = 9;
    private const string Pattern = @"^\+?[1-9]\d{1,14}$";

    [JsonConstructor]

    private PhoneNumber(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static Result<PhoneNumber> Create(string value)
    {
        if (string.IsNullOrEmpty(value) || !PhoneNumberRegex().IsMatch(value) || value.Length != DefaultLenght)
        {
            return Result.Failure<PhoneNumber>(DomainErrors.PhoneNumberErrors.InvalidFormat);
        }

        return new PhoneNumber(value);
    }

    [GeneratedRegex(Pattern)]
    private static partial Regex PhoneNumberRegex();

}
