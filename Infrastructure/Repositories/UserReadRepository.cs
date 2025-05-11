
using Application.UseCaseUser.IRepositories;
using Application.UseCaseUser.ResponseDTos;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Models;
using MongoDB.Driver;


namespace Infrastructure.Repositories;

public class UserReadRepository : IUserReadRepository
{
    private readonly MongoDbContext _dbContext;

    public UserReadRepository(MongoDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<UserResponse?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        UserReadModel? user = await _dbContext.Users
            .Find(u => u.Id == id)
            .FirstOrDefaultAsync(cancellationToken);

        if (user != null)
            return (MapToDto(user));

        return null;
    }

    public async Task<List<UserResponse>> GetAllAsync(CancellationToken cancellationToken)
    {
        List<UserReadModel> users = await _dbContext.Users
            .Find(_ => true)
            .ToListAsync(cancellationToken);

        return users.Select(MapToDto).ToList();
    }

    public async Task<UserContactInfoResponse?> GetContactInfoAsync(Guid id, CancellationToken cancellationToken)
    {
        UserReadModel? user = await _dbContext.Users
            .Find(u => u.Id == id)
            .FirstOrDefaultAsync(cancellationToken);

        if (user != null)
        {
            var contactInfo = new UserContactInfoReadModel
            {
                Id = user.Id,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Address = user.Address
            };

            return MapToContactInfoDto(contactInfo);
        }

        return null;
    }




    /*  public async Task<UserResponse> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
      {
          return await _dbContext.Users
              .Find(u => u.Email == email)
              .FirstOrDefaultAsync(cancellationToken);
      }
    */

    private UserResponse MapToDto(UserReadModel document)
    {
        return new UserResponse
        {
            Id = document.Id,
            FirstName = document.FirstName,
            LastName = document.LastName,
            Email = document.Email,
            PhoneNumber = document.PhoneNumber,
            Address = new AddressResponse
            {
                Country = document.Address.Country,
                Street = document.Address.Street,
                City = document.Address.City,
                State = document.Address.State,
                ZipCode = document.Address.ZipCode
            }
        };
    }

    private UserContactInfoResponse MapToContactInfoDto(UserContactInfoReadModel document)
    {
        return new UserContactInfoResponse
        {
            Id = document.Id,

            Email = document.Email,
            PhoneNumber = document.PhoneNumber,
            Address = new AddressResponse
            {
                Country = document.Address.Country,
                Street = document.Address.Street,
                City = document.Address.City,
                State = document.Address.State,
                ZipCode = document.Address.ZipCode
            }
        };
    }


}

