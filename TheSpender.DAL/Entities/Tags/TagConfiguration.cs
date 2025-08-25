using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TheSpender.DAL.Entities.Tags;

public class TagConfiguration : BaseEntityConfiguration, IEntityTypeConfiguration<Tag>
{
    public void Configure(EntityTypeBuilder<Tag> builder)
    {
        builder.Property(e => e.Name).IsRequired();
    }
}
