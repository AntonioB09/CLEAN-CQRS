using Domain.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations.Write;

internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);

        builder.Property(u => u.Id).HasConversion(
                userId => userId.Value,
                value => new UserId(value));

        builder.ComplexProperty(
            u => u.Email,
            b => b.Property(e => e.Value).HasColumnName(nameof(User.Email)));

        builder.ComplexProperty(
            u => u.FirstName,
            b => b.Property(e => e.Value).HasColumnName(nameof(User.FirstName)));

        builder.ComplexProperty(
            u => u.LastName,
            b => b.Property(e => e.Value).HasColumnName(nameof(User.LastName)));

        builder.ComplexProperty(
             u => u.PhoneNumber,
             b => b.Property(e => e.Value).HasColumnName(nameof(User.PhoneNumber)));

        builder.ComplexProperty(
            u => u.Address,
            b =>
            {
                b.Property(a => a.Country).HasColumnName(nameof(User.Address.Country));
                b.Property(a => a.Street).HasColumnName(nameof(User.Address.Street));
                b.Property(a => a.City).HasColumnName(nameof(User.Address.City));
                b.Property(a => a.State).HasColumnName(nameof(User.Address.State));
                b.Property(a => a.ZipCode).HasColumnName(nameof(User.Address.ZipCode));
            });


    }
}


