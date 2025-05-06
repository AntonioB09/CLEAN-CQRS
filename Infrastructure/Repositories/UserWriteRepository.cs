
using Domain.User;
using Domain.User.ValueObjects;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

internal sealed class UserWriteRepository : IUserWriteRepository
{
    private readonly ApplicationWriteDbContext _dbContext;

    public UserWriteRepository(ApplicationWriteDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<User?> GetByIdAsync(UserId id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.UsersEvent.FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
    }

    public async Task<bool> IsEmailUniqueAsync(Email email)
    {
        return !await _dbContext.UsersEvent.AnyAsync(u => u.Email == email);
    }

    public void Insert(User user)
    {
        _dbContext.UsersEvent.Add(user);
    }

    public async Task<bool> ExistsAsync(UserId id) => await _dbContext.UsersEvent.AnyAsync(customer => customer.Id == id);

    public void UpdateUser(User user)
    {
        _dbContext.UsersEvent.Update(user);
    }

}
