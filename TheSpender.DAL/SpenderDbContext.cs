using Microsoft.EntityFrameworkCore;
using TheSpender.DAL.Entities;

namespace TheSpender.DAL;

public class SpenderDbContext(DbContextOptions<SpenderDbContext> options): DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<Operation> Operations { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Tag> Tags { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<User>(entity =>
        {
            entity.HasKey(u => u.Id);
            entity.Property(u => u.ClientId).IsRequired();
            entity.Property(u => u.CreatedOn).IsRequired();
        });

        builder.Entity<Category>(entity =>
        {
            entity.HasKey(c => c.Id);
            entity.Property(c => c.Name).IsRequired().HasMaxLength(100);
            entity.Property(c => c.CategoryType).IsRequired();
            entity.Property(c => c.IsDefault).HasDefaultValue(false);
            entity.Property(c => c.IsDeleted).HasDefaultValue(false);

            entity.HasOne<User>()
                  .WithMany()
                  .HasForeignKey(c => c.UserId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        builder.Entity<Operation>(entity =>
        {
            entity.HasKey(o => o.Id);
            entity.Property(o => o.Description).IsRequired().HasMaxLength(300);
            entity.Property(o => o.Date).IsRequired();
            entity.Property(o => o.SumOfMoney).IsRequired();
            entity.Property(o => o.OperationNumber).IsRequired();
            entity.Property(o => o.IsDeleted).HasDefaultValue(false);

            entity.HasOne<User>()
                  .WithMany()
                  .HasForeignKey(o => o.UserId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne<Category>()
                  .WithMany()
                  .HasForeignKey(o => o.CategoryId)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        builder.Entity<Tag>(entity =>
        {
            entity.HasKey(t => t.Id);
            entity.Property(t => t.Name).IsRequired().HasMaxLength(50);

            entity.HasOne<Category>()
                  .WithMany()
                  .HasForeignKey(t => t.CategoryId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        base.OnModelCreating(builder);
    }
}
