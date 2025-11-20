using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics.CodeAnalysis;
using WaracleHotelBooking.DataModel.Data;

namespace WaracleHotelBooking.Tests.Utils;

[ExcludeFromCodeCoverage]
public static class TestDbContextFactory
{
    public static AppDbContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()) // fresh DB for each test
            .Options;

        var db = new AppDbContext(options);
        DbSeeder.Seed(db); // deterministic seed
        return db;
    }
}