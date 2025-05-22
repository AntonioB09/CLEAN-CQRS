using Domain.Errors;
using Domain.Shared;
using System.Text.RegularExpressions;

namespace Domain.User.ValueObjects;

public sealed record Email
{
    private static readonly Regex EmailRegex = new Regex(@"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$", RegexOptions.Compiled);

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

        if (!EmailRegex.IsMatch(email))
        {
            return Result.Failure<Email>(DomainErrors.EmailErrors.InvalidFormat);
        }

        return new Email(email);
    }

  
}