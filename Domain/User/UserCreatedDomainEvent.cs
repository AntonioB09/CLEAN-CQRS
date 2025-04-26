

using Domain.Shared;
using Domain.User;

namespace Domain.Users;

public sealed record UserCreatedDomainEvent(UserId id) : IDomainEvent;
