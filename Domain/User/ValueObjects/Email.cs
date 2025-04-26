using Domain.Errors;
using Domain.Shared;

namespace Domain.User.ValueObjects;

public sealed record Email
{
    private Email(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static Result<Email> Create(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            return Result.Failure<Email>(DomainErrors.EmailErrors.Empty);
        }

        if (email.Split('@').Length != 2)
        {
            return Result.Failure<Email>(DomainErrors.EmailErrors.InvalidFormat);
        }

        return new Email(email);
    }

  
}