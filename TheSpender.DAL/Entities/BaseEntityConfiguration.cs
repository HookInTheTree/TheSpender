using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TheSpender.DAL.Entities
{
    internal abstract class BaseEntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity>
        where TEntity : BaseEntity
    {
        /// <summary>
        /// Метод для конфигурации наследника.
        /// </summary>
        /// <param name="builder"></param>
        protected abstract void ConfigureEntity(EntityTypeBuilder<TEntity> builder);

        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.Property(e => e.CreatedOn).HasDefaultValueSql("NOW() AT TIME ZONE 'UTC'");
            builder.Property(e => e.ModifiedOn).HasDefaultValueSql("NOW() AT TIME ZONE 'UTC'");
            builder.Property(e => e.IsDeleted).HasDefaultValue(false);

            builder.HasQueryFilter(e => e.IsDeleted == false);
            ConfigureEntity(builder);
        }
    }
}
