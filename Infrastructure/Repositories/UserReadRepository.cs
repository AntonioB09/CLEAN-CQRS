
using Infrastructure.Data;
using Infrastructure.Data.Models;
using MongoDB.Driver;
using Application.UseCaseUser;
using Domain.User;
using System.Threading;
using ZstdSharp;

namespace Infrastructure.Repositories;

public class UserReadRepository : IUserReadRepository
{
    private readonly MongoDbContext _dbContext;

    public UserReadRepository(MongoDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<UserResponse>GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var filter = Builders<UserReadModel>.Filter.Eq(u => u.Id, id);
        var user = await _dbContext.Users
            .Find(u => u.Id == id)
            .FirstOrDefaultAsync(cancellationToken);

    
        return MapToDto(user);
    }


    public async Task<List<UserResponse>> GetAllAsync(CancellationToken cancellationToken)
    {
        var users = await _dbContext.Users
            .Find(_ => true)
            .ToListAsync(cancellationToken);

        return users.Select(MapToDto).ToList();
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


} 
 
