using Domain.User;
using Domain.User.ValueObjects;
using Infrastructure.Data;
using MongoDB.Driver;


namespace Infrastructure.Repositories
{
    public class UserReadRepository : IUserReadRepository
    {
        private readonly MongoDbContext _dbContext;

        public UserReadRepository(MongoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User?> GetByIdAsync(UserId id, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Users
                .Find(u => u.Id == id)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<User?> GetByEmailAsync(Email email, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Users
                .Find(u => u.Email == email)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<IEnumerable<User?>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.Users
                .Find(_ => true)
                .ToListAsync(cancellationToken);
        }
    }
}
