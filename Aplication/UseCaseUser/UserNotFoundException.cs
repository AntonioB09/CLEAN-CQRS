using Domain.User;

namespace Application.UseCaseUser;

public sealed class UserNotFoundException : Exception
{
    public UserNotFoundException(UserId userId)
        : base($"The user with the identifier {userId} was not found")
    {

    }
}
