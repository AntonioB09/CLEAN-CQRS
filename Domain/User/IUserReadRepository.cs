

using Domain.User.ValueObjects;

namespace Domain.User
{
    public interface IUserReadRepository
    {
        Task<User?> GetByIdAsync(UserId id, CancellationToken cancellationToken = default);
        Task<User?> GetByEmailAsync(Email email, CancellationToken cancellationToken = default);
        Task<IEnumerable<User?>> GetAllAsync(CancellationToken cancellationToken = default);
    }
}
