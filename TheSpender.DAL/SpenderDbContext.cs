using Microsoft.EntityFrameworkCore;

namespace TheSpender.DAL.Tests;

public class SpenderDbContext(DbContextOptions<SpenderDbContext> options): DbContext(options)
{
}
