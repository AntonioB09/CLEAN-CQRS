using Domain.User;
using Infrastructure.Data.Models;
using Infrastructure.Data;
using MassTransit;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;


namespace Infrastructure.RabbitMQ;

public sealed class UserUpdatedEventConsumer : IConsumer<UserUpdatedDomainEvent>
{
    private readonly ILogger<UserUpdatedEventConsumer> _logger;
    private readonly MongoDbContext _dbContext;

    public UserUpdatedEventConsumer(ILogger<UserUpdatedEventConsumer> logger, MongoDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    public async Task Consume(ConsumeContext<UserUpdatedDomainEvent> context)
    {
        _logger.LogInformation("Processing UserUpdatedDomainEvent: {@User}", context.Message);


        // Map the domain event to the mongo model
        var user = new UserReadModel
        {
            Id = context.Message.id.Value,
            FirstName = context.Message.FirstName.Value,
            LastName = context.Message.LastName.Value,
            Email = context.Message.Email.Value,
            PhoneNumber = context.Message.PhoneNumber.Value,
            Address = new AddressReadModel
            {
                Street = context.Message.Address.Street,
                City = context.Message.Address.City,
                State = context.Message.Address.State,
                ZipCode = context.Message.Address.ZipCode,
                Country = context.Message.Address.Country
            }
        };

        var filter = Builders<UserReadModel>.Filter.Eq(u => u.Id, user.Id);
        var userUpdate = Builders<UserReadModel>.Update
            .Set(u => u.FirstName, user.FirstName)
            .Set(u => u.LastName, user.LastName)
            .Set(u => u.Email, user.Email)
            .Set(u => u.PhoneNumber, user.PhoneNumber)
            .Set(u => u.Address.Country, user.Address.Country)
            .Set(u => u.Address.State, user.Address.State)
            .Set(u => u.Address.City, user.Address.City)
            .Set(u => u.Address.Street, user.Address.Street)
            .Set(u => u.Address.ZipCode, user.Address.ZipCode);


        // Save the user to MongoDB
        await _dbContext.Users.UpdateOneAsync(filter, userUpdate);

        _logger.LogInformation("User update to MongoDB: {@User}", user);
    }
}

