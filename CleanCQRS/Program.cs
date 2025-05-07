using Application;
using Application.Messaging;
using CleanCQRS.Controllers;
using Domain.User;
using Infrastructure;
using Infrastructure.Repositories;
using Infrastructure.Data;
using Infrastructure.RabbitMQ;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Application.UseCaseUser;

var builder = WebApplication.CreateBuilder(args);


builder.Services.Configure<MessageBrokerSettings>( 
    builder.Configuration.GetSection("RabbitMQ"));

builder.Services.AddSingleton(Sp => 
Sp.GetRequiredService<IOptions<MessageBrokerSettings>>().Value);

builder.Services.AddMassTransit(busconfiguation =>
{
    busconfiguation.SetKebabCaseEndpointNameFormatter();

    busconfiguation.AddConsumer<UserCreatedEventConsumer>();
    busconfiguation.AddConsumer<UserUpdatedEventConsumer>();

    busconfiguation.UsingRabbitMq((context, configurator) =>
    {
        MessageBrokerSettings settings = context.GetRequiredService<MessageBrokerSettings>();

        configurator.Host(new Uri(settings.Host), h =>
        {
            h.Username(settings.Username);
            h.Password(settings.Password);

        });
        configurator.ReceiveEndpoint("user-created-event-queue", endpoint =>
        {
            endpoint.ConfigureConsumer<UserCreatedEventConsumer>(context);
        });
        configurator.ReceiveEndpoint("user-updated-event-queue", endpoint =>
        {
            endpoint.ConfigureConsumer<UserUpdatedEventConsumer>(context);
        });
    });
});

builder.Services.AddTransient<IEventBus, EventBus>();


builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration);
builder.Services.AddTransient<IUserReadRepository, UserReadRepository>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.MapUserEndpoints();

app.Run();
