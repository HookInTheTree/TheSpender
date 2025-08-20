using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheSpender.DAL.Entities.Categories;

namespace TheSpender.DAL.Entities.Tags;

public class TagConfiguration : IEntityTypeConfiguration<Tag>
{
    public void Configure(EntityTypeBuilder<Tag> builder)
    {
        builder.Property(t => t.Name).IsRequired();
        builder.Property(e => e.CreatedOn).HasDefaultValueSql("GETUTCDATE()");
        builder.Property(e => e.ModifiedOn).HasDefaultValueSql("GETUTCDATE()");
        builder.Property(e => e.IsDeleted).HasDefaultValue(false);

        builder.HasOne<Category>()
              .WithMany()
              .HasForeignKey(t => t.CategoryId)
              .OnDelete(DeleteBehavior.Cascade);
    }
}
