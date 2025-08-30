using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TheSpender.DAL.Entities.Categories;

internal sealed class CategoryConfiguration : BaseEntityConfiguration<Category>
{
    protected override void ConfigureEntity(EntityTypeBuilder<Category> builder)
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
