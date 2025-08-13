using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheSpender.DAL.Entities.Categories;

namespace TheSpender.DAL.Entities.Tags;

public class TagConfiguration : IEntityTypeConfiguration<Tag>
{
    public void Configure(EntityTypeBuilder<Tag> builder)
    {
        builder.Property(t => t.Name).IsRequired();

        builder.HasOne<Category>()
              .WithMany()
              .HasForeignKey(t => t.CategoryId)
              .OnDelete(DeleteBehavior.Cascade);
    }
}
