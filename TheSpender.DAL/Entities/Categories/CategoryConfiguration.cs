using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheSpender.DAL.Entities.Users;

namespace TheSpender.DAL.Entities.Categories;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.Property(e => e.Name).IsRequired();
        builder.Property(e => e.CategoryType).IsRequired();
        builder.Property(e => e.IsDefault).HasDefaultValue(false);
        builder.Property(e => e.IsDeleted).HasDefaultValue(false);
        builder.Property(e => e.CreatedOn).HasDefaultValueSql("GETUTCDATE()");
        builder.Property(e => e.ModifiedOn).HasDefaultValueSql("GETUTCDATE()");

        builder.HasIndex(e => new { e.UserId, e.CategoryType });

        builder.HasOne(e => e.User)
                .WithMany()
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired(false);
    }
}
