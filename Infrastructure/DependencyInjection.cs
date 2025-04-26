

using Application.Data;
using Domain.User;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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

        services.AddTransient(_ => new DbConnectionFactory(connectionString));

        services.AddDbContext<ApplicationWriteDbContext>(
          options => options.UseNpgsql(connectionString)
          );

        services.AddSingleton<PublishDomainEventsInterceptor>();



        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ApplicationWriteDbContext>());


        services.AddScoped<IUserRepository, UserRepository>();


    }
}
