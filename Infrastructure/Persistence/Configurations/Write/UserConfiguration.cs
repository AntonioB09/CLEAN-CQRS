using Domain.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations.Write;

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
        builder.HasIndex(u => u.Email.Value).IsUnique();
        builder.Property(u => u.Email.Value).HasMaxLength(20);

        
        builder.ComplexProperty(
            u => u.FirstName,
            b => b.Property(e => e.Value).HasColumnName(nameof(User.FirstName)));
        builder.Property(u => u.FirstName.Value).HasMaxLength(50);


        builder.ComplexProperty(
            u => u.LastName,
            b => b.Property(e => e.Value).HasColumnName(nameof(User.LastName)));
        builder.Property(u => u.LastName.Value).HasMaxLength(50);


        builder.ComplexProperty(
             u => u.PhoneNumber,
             b => b.Property(e => e.Value).HasColumnName(nameof(User.PhoneNumber)));
        builder.Property(u => u.PhoneNumber.Value).HasMaxLength(9);


        builder.ComplexProperty(
            u => u.Address,
            b =>
            {
                b.Property(a => a.Country).HasColumnName(nameof(User.Address.Country));
                builder.Property(u => u.Address.Country).HasMaxLength(20);

                b.Property(a => a.State).HasColumnName(nameof(User.Address.State));
                builder.Property(u => u.Address.State).HasMaxLength(20);

                b.Property(a => a.City).HasColumnName(nameof(User.Address.City));
                builder.Property(u => u.Address.City).HasMaxLength(20);

                b.Property(a => a.Street).HasColumnName(nameof(User.Address.Street));
                builder.Property(u => u.Address.Street).HasMaxLength(20);

                b.Property(a => a.ZipCode).HasColumnName(nameof(User.Address.ZipCode));
                builder.Property(u => u.Address.ZipCode).HasMaxLength(20);

            });


    }
}


