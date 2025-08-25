using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TheSpender.DAL.Entities.Users;

public class UserConfiguration : BaseEntityConfiguration, IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(u => u.ClientId).IsRequired();

        builder.HasIndex(u => u.ClientId)
            .IsUnique();
    }
}
