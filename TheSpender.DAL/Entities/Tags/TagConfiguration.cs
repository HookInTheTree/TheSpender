using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TheSpender.DAL.Entities.Tags;

internal sealed class TagConfiguration : BaseEntityConfiguration<Tag>
{
    protected override void ConfigureEntity(EntityTypeBuilder<Tag> builder)
    {
        builder.Property(e => e.Name).IsRequired();
    }
}
