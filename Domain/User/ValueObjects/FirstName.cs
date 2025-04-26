using Domain.Errors;
using Domain.Primitives;
using Domain.Shared;

namespace Domain.User.ValueObjects;

public sealed record FirstName 
{
    public const int MaxLength = 50;

    private FirstName(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static Result<FirstName> Create(string firstName)
    {
        if (string.IsNullOrWhiteSpace(firstName))
        {
            return Result.Failure<FirstName>(DomainErrors.FirstNameErrors.Empty);
        }

        if (firstName.Length > MaxLength)
        {
            return Result.Failure<FirstName>(DomainErrors.FirstNameErrors.TooLong);
        }

        return new FirstName(firstName);
    }

   
}