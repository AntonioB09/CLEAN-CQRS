
using Domain.User;
using Domain.User.ValueObjects;
using Infrastructure.Data;
using MassTransit;
using MassTransit.Middleware;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using static MassTransit.Transports.ReceiveEndpoint;
using System.IO;
using System.Reflection.Emit;
using Infrastructure.Data.Models;

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

        // Map the domain event to the User entity
        var user = new UserReadModel
        {
            Id = context.Message.id.Value,
            FirstName = context.Message.FirstName.Value,

        };

        // Save the user to MongoDB
        await _dbContext.Users.InsertOneAsync(user);

        _logger.LogInformation("User saved to MongoDB: {@User}", user);
    }
}
