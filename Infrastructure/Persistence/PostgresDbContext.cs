
using Application.Data;
using Domain.User;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Persistence;

public sealed class PostgresDbContext : DbContext, IUnitOfWork
{
    public PostgresDbContext(DbContextOptions<PostgresDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> UsersEvent { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(PostgresDbContext).Assembly,
            WriteConfigurationsFilter);
    }

    private static bool WriteConfigurationsFilter(Type type) =>
        type.FullName?.Contains("Configurations.Write") ?? false;
}
