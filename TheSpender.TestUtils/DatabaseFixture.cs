using Microsoft.EntityFrameworkCore;
using TheSpender.DAL;

namespace TheSpender.TestUtils;

/// <summary>
/// Фикстура для тестов, использующих базу данных.
/// На каждый тест-класс создаётся база данных, после прогона тестов в нём, база дроппается.
/// </summary>
public class DatabaseFixture : IAsyncDisposable
{
    public SpenderDbContext DbContext { get; }

    public DatabaseFixture()
    {
        DbContext = new SpenderDbContext(new DbContextOptionsBuilder<SpenderDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options);
        DbContext.Database.EnsureCreated();
    }

    /// <summary>
    /// Performs application-defined tasks associated with freeing, releasing, or
    /// resetting unmanaged resources asynchronously.</summary>
    /// <returns>A task that represents the asynchronous dispose operation.</returns>
#pragma warning disable CA1816
    public async ValueTask DisposeAsync()
#pragma warning restore CA1816
    {
        await DbContext.Database.EnsureDeletedAsync();
        await DbContext.DisposeAsync();
    }
}
