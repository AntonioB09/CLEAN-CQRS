using Domain.User.ValueObjects;

namespace Application.UseCaseUser.GetByEmail;

public sealed record UserResponse
{
    public Guid Id { get; init; }

    public required Email Email { get; init; }

    public required FirstName FirstName { get; init; }

    public required LastName LastName { get; init; }



}
