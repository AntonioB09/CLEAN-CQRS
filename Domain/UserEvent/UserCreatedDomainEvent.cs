using Domain.Shared;
using Domain.User;
using Domain.User.ValueObjects;


namespace Domain.UserEvent;

public sealed record UserCreatedDomainEvent(
    UserId id,
    FirstName FirstName,
    LastName LastName,
    Email Email,
    PhoneNumber PhoneNumber,
    Address Address
    ) : IDomainEvent;
