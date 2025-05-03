using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Infrastructure.Data;

public class ApplicationWriteDbContextFactory : IDesignTimeDbContextFactory<ApplicationWriteDbContext>
{
    public ApplicationWriteDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationWriteDbContext>();
        optionsBuilder.UseNpgsql("Host=localhost; Database=BdTest; Username=postgres; Password=2061368090");

        return new ApplicationWriteDbContext(optionsBuilder.Options);
    }
}
