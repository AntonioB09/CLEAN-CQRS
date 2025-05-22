using Domain.Errors;
using Domain.Shared;
using System.Text.RegularExpressions;


namespace Domain.User.ValueObjects;

public sealed partial record PhoneNumber 
{
    private const string Pattern = @"^[0-9]{9}$";

    private PhoneNumber(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static Result<PhoneNumber> Create(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return Result.Failure<PhoneNumber>(DomainErrors.PhoneNumberErrors.Empty);
        }

        if (!PhoneNumberRegex().IsMatch(value))
        {
            return Result.Failure<PhoneNumber>(DomainErrors.PhoneNumberErrors.InvalidFormat);
        }

        return new PhoneNumber(value);
    }

    [GeneratedRegex(Pattern)]
    private static partial Regex PhoneNumberRegex();

}
