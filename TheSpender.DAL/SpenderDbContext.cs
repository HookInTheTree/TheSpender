using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure;
using System.Reflection;
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
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(builder);
    }

    public static void MapEnums(NpgsqlDbContextOptionsBuilder options)
    {
        options.MapEnum<CategoryTypes>();
    }
}
