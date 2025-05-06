

using Application.Data;
using Domain.User;
using Infrastructure.Data;
using Infrastructure.RabbitMQ;
using Infrastructure.Repositories;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.VisualBasic.FileIO;
using Shared;


namespace Infrastructure;

public static class DependencyInjection
{
    public static void AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

        services.AddMediatR(config =>
            config.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));

        string? connectionString = configuration.GetConnectionString("Database");
        Ensure.NotNullOrEmpty(connectionString);

        string? mongoConnectionString = configuration.GetConnectionString("MongoDB");
        Ensure.NotNullOrEmpty(mongoConnectionString, nameof(mongoConnectionString));
        string? mongoDatabaseName = configuration["MongoDB:DatabaseName"];
        Ensure.NotNullOrEmpty(mongoDatabaseName, nameof(mongoDatabaseName));
        services.AddSingleton(provider =>
            new MongoDbContext(
                mongoConnectionString,
                mongoDatabaseName
            ));


        /* Configuración de MongoDB
        services.AddSingleton(provider =>
            new MongoDbContext(
                configuration.GetConnectionString("MongoDB"),
                configuration["MongoDB:DatabaseName"]
            ));*/

        services.AddDbContext<ApplicationWriteDbContext>(options =>
        {
            options.UseNpgsql(connectionString);
        });
       

 

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ApplicationWriteDbContext>());



        services.AddScoped<IUserWriteRepository, UserWriteRepository>();
      


    }

}


