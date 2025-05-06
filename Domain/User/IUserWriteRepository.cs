
using Domain.User.ValueObjects;

namespace Domain.User;
public interface IUserWriteRepository
{
    void Insert(User user);

    Task<User?> GetByIdAsync(UserId id, CancellationToken cancellationToken = default);

    Task<bool> IsEmailUniqueAsync(Email email);

    Task<bool> ExistsAsync(UserId id);

    void UpdateUser(User user);



}
