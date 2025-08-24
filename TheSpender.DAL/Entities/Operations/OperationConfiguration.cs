using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheSpender.DAL.Entities.Categories;
using TheSpender.DAL.Entities.Users;

namespace TheSpender.DAL.Entities.Operations;

public class OperationConfiguration : IEntityTypeConfiguration<Operation>
{
    public void Configure(EntityTypeBuilder<Operation> builder)
    {
        builder.Property(e => e.Description).IsRequired();
        builder.Property(e => e.Date).IsRequired();
        builder.Property(e => e.SumOfMoney).IsRequired();
        builder.Property(e => e.OperationNumber).IsRequired();
        builder.Property(e => e.IsDeleted).HasDefaultValue(false);
        builder.Property(e => e.CreatedOn).HasDefaultValueSql("GETUTCDATE()");
        builder.Property(e => e.ModifiedOn).HasDefaultValueSql("GETUTCDATE()");


        builder.HasIndex(e => e.OperationNumber).IsUnique();
    }
}
