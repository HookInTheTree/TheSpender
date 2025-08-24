using Microsoft.EntityFrameworkCore;
using TheSpender.DAL;

namespace TheSpender.TestUtils;

/// <summary>
/// Фикстура для тестов, использующих базу данных.
/// На каждый тест-класс создаётся база данных, после прогона тестов в нём, база дроппается.
/// </summary>
public static class DatabaseFixture
{
    public static SpenderDbContext CreateContext()
    {
        return new SpenderDbContext(new DbContextOptionsBuilder<SpenderDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options);
    }
}
