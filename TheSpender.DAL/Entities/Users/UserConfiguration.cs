using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TheSpender.DAL.Entities.Users;

internal sealed class UserConfiguration : BaseEntityConfiguration<User>
{
    protected override void ConfigureEntity(EntityTypeBuilder<User> builder)
    {
        builder.Property(u => u.ClientId).IsRequired();

        builder.HasIndex(u => u.ClientId)
            .IsUnique();
    }
}
