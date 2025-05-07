
using Domain.User;

using Infrastructure.Data;
using MassTransit;
using MassTransit.Middleware;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using static MassTransit.Transports.ReceiveEndpoint;
using System.IO;
using System.Reflection.Emit;
using Infrastructure.Data.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Infrastructure.RabbitMQ;

public sealed class UserCreatedEventConsumer : IConsumer<UserCreatedDomainEvent>
{
    private readonly ILogger<UserCreatedEventConsumer> _logger;
    private readonly MongoDbContext _dbContext;

    public UserCreatedEventConsumer(ILogger<UserCreatedEventConsumer> logger, MongoDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    public async Task Consume(ConsumeContext<UserCreatedDomainEvent> context)
    {
        _logger.LogInformation("User Created : {@User}", context.Message);


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

        // Save the user to MongoDB
        await _dbContext.Users.InsertOneAsync(user);

        _logger.LogInformation("User update to MongoDB: {@User}", user);
    }
}

