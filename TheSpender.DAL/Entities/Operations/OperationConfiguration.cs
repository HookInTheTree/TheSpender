using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TheSpender.DAL.Entities.Operations;

internal sealed class OperationConfiguration : BaseEntityConfiguration<Operation>
{
    protected override void ConfigureEntity(EntityTypeBuilder<Operation> builder)
    {
        builder.Property(e => e.Description).IsRequired();
        builder.Property(e => e.Date).IsRequired();
        builder.Property(e => e.SumOfMoney).IsRequired();
        builder.Property(e => e.OperationNumber).IsRequired();

        builder.HasIndex(e => new { e.UserId, e.OperationNumber }).IsUnique();
    }
}
