using Microsoft.EntityFrameworkCore;

namespace TheSpender.DAL;

public class SpenderDbContext(DbContextOptions<SpenderDbContext> options): DbContext(options)
{
}
