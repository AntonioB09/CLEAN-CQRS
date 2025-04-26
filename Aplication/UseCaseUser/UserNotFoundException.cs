namespace Application.UseCaseUser;

public sealed class UserNotFoundException : Exception
{
    public UserNotFoundException(Guid userId)
        : base($"The user with the identifier {userId} was not found")
    {

    }
}
