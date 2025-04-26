namespace Infrastructure.Data.Models;

internal sealed class UserReadModel
{
    public Guid Id { get; set; }

    public required string Name { get; set; }

    public required string Email { get; set; }


}
