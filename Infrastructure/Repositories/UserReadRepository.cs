
using Infrastructure.Data;
using Infrastructure.Data.Models;
using MongoDB.Driver;
using Application.UseCaseUser;
using Domain.User;
using System.Threading;
using ZstdSharp;
using Domain.Shared;
using Domain.User.ValueObjects;
using System.Collections.Generic;
using static MassTransit.ValidationResultExtensions;

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
        var user = await _dbContext.Users
            .Find(u => u.Id == id)
            .FirstOrDefaultAsync(cancellationToken);

        if (user != null)
            return MapToDto(user);
        return null;

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
 
