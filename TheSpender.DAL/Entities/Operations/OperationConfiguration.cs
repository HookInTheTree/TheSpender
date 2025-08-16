using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheSpender.DAL.Entities.Categories;
using TheSpender.DAL.Entities.Users;

namespace TheSpender.DAL.Entities.Operations;

public class OperationConfiguration : IEntityTypeConfiguration<Operation>
{
    public void Configure(EntityTypeBuilder<Operation> builder)
    {
        builder.Property(o => o.Description).IsRequired();
        builder.Property(o => o.Date).IsRequired();
        builder.Property(o => o.SumOfMoney).IsRequired();
        builder.Property(o => o.OperationNumber).IsRequired();
        builder.Property(o => o.IsDeleted).HasDefaultValue(false);
        builder.Property(e => e.CreatedOn).HasDefaultValueSql("GETUTCDATE()");
        builder.Property(e => e.ModifiedOn).HasDefaultValueSql("GETUTCDATE()");

        builder.HasOne<User>()
              .WithMany()
              .HasForeignKey(o => o.UserId)
              .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne<Category>()
              .WithMany()
              .HasForeignKey(o => o.CategoryId)
              .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(o => o.OperationNumber).IsUnique();
    }
}
