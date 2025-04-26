using Domain.Errors;
using Domain.Shared;

namespace Domain.User.ValueObjects;

public sealed record LastName 
{
    public const int MaxLength = 50;

    private LastName(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static Result<LastName> Create(string lastName)
    {
        if (string.IsNullOrWhiteSpace(lastName))
        {
            return Result.Failure<LastName>(DomainErrors.LastNameErrors.Empty);
        }

        if (lastName.Length > MaxLength)
        {
            return Result.Failure<LastName>(DomainErrors.LastNameErrors.TooLong);
        }

        return new LastName(lastName);
    }


}