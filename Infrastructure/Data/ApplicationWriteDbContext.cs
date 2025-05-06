
using Application.Data;
using Domain.User;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Data;

public sealed class ApplicationWriteDbContext : DbContext, IUnitOfWork
{
    public ApplicationWriteDbContext(DbContextOptions<ApplicationWriteDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> UsersEvent { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(ApplicationWriteDbContext).Assembly,
            WriteConfigurationsFilter);
    }

    private static bool WriteConfigurationsFilter(Type type) =>
        type.FullName?.Contains("Configurations.Write") ?? false;
}
