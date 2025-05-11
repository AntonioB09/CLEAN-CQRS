using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Infrastructure.Persistence;

public class ApplicationWriteDbContextFactory : IDesignTimeDbContextFactory<PostgresDbContext>
{
    public PostgresDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<PostgresDbContext>();
        optionsBuilder.UseNpgsql("Host=localhost; Database=BdTest; Username=postgres; Password=2061368090");

        return new PostgresDbContext(optionsBuilder.Options);
    }
}
