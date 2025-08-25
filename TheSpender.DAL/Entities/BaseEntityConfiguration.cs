using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TheSpender.DAL.Entities
{
    public class BaseEntityConfiguration
    {
        public void Configure(EntityTypeBuilder<BaseEntity> builder)
        {
            builder.Property(e => e.CreatedOn).HasDefaultValueSql("NOW() AT TIME ZONE 'UTC'");
            builder.Property(e => e.ModifiedOn).HasDefaultValueSql("NOW() AT TIME ZONE 'UTC'");
            builder.Property(e => e.IsDeleted).HasDefaultValue(false);

            builder.HasQueryFilter(e => e.IsDeleted == false);
        }
    }
}
