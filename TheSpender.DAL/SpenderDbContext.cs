using Microsoft.EntityFrameworkCore;
using TheSpender.DAL.Entities;
using TheSpender.DAL.Entities.Categories;
using TheSpender.DAL.Entities.Operations;
using TheSpender.DAL.Entities.Tags;
using TheSpender.DAL.Entities.Users;

namespace TheSpender.DAL;

public class SpenderDbContext(DbContextOptions<SpenderDbContext> options): DbContext(options)
{
    public SpenderDbContext() : this(new DbContextOptions<SpenderDbContext>())
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Operation> Operations { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Tag> Tags { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<BaseEntity>(entity =>
        {
            entity.HasKey(u => u.Id);
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("GETUTCDATE()");
            entity.Property(e => e.ModifiedOn).HasDefaultValueSql("GETUTCDATE()");
        });

        builder.HasPostgresEnum<CategoryTypes>();

        base.OnModelCreating(builder);
    }
}
