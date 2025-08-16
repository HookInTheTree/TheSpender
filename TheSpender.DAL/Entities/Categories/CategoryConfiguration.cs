using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheSpender.DAL.Entities.Users;

namespace TheSpender.DAL.Entities.Categories;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.Property(c => c.Name).IsRequired();
        builder.Property(c => c.CategoryType).IsRequired();
        builder.Property(c => c.IsDefault).HasDefaultValue(false);
        builder.Property(c => c.IsDeleted).HasDefaultValue(false);
        builder.Property(e => e.CreatedOn).HasDefaultValueSql("GETUTCDATE()");
        builder.Property(e => e.ModifiedOn).HasDefaultValueSql("GETUTCDATE()");

        builder.HasOne<User>()
                .WithMany()
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);
    }
}
