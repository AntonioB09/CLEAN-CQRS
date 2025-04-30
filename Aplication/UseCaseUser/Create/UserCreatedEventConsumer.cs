

using MassTransit;
using Microsoft.Extensions.Logging;

namespace Application.UseCaseUser.Create;

public sealed class UserCreatedEventConsumer : IConsumer<UserCreatedEvent>
{
    private readonly ILogger<UserCreatedEventConsumer> _logger;

    public UserCreatedEventConsumer(ILogger<UserCreatedEventConsumer> logger)
    {
        _logger = logger;
    }

    public Task Consume(ConsumeContext<UserCreatedEvent> context)
    {
        _logger.LogInformation("User Created : {@User}", context.Message);

        return Task.CompletedTask;
    }
}
