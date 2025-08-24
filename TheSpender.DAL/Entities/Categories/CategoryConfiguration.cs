using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TheSpender.DAL.Entities.Categories;

public class CategoryConfiguration : BaseEntityConfiguration, IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.Property(e => e.Name).IsRequired();
        builder.Property(e => e.CategoryType).IsRequired();
        builder.Property(e => e.IsDefault).HasDefaultValue(false);

        builder.HasIndex(e => new { e.UserId, e.CategoryType });

        builder.HasOne(e => e.User)
                .WithMany()
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired(false);
    }
}
