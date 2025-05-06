

using Domain.Shared;
using Domain.User.ValueObjects;


namespace Domain.User;

public sealed record UserCreatedDomainEvent(
    UserId id, 
    FirstName FirstName,
    LastName LastName,
    Email Email,
    PhoneNumber PhoneNumber,
    Address Address
    ) : IDomainEvent;
