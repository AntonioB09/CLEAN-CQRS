
using Domain.User.ValueObjects;

namespace Domain.User;
public interface IUserRepository
{
    Task<User?> GetByIdAsync(UserId id, CancellationToken cancellationToken = default);

    Task<bool> IsEmailUniqueAsync(Email email);

    void Insert(User user);
}
