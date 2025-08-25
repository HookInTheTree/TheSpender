using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TheSpender.DAL.Entities.Operations;

public class OperationConfiguration : BaseEntityConfiguration, IEntityTypeConfiguration<Operation>
{
    public void Configure(EntityTypeBuilder<Operation> builder)
    {
        builder.Property(e => e.Description).IsRequired();
        builder.Property(e => e.Date).IsRequired();
        builder.Property(e => e.SumOfMoney).IsRequired();
        builder.Property(e => e.OperationNumber).IsRequired();


        builder.HasIndex(e => e.OperationNumber).IsUnique();
    }
}
