using FreedomPlanner.Application.Services;
using FreedomPlanner.Infrastructure.Data;
using FreedomPlanner.Infrastructure.Repositories;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace FreedomPlanner.Tests;

public sealed class UserPlanServiceTests : IDisposable
{
    private readonly SqliteConnection _connection;
    private readonly DbContextOptions<AppDbContext> _options;

    public UserPlanServiceTests()
    {
        _connection = new SqliteConnection("DataSource=:memory:");
        _connection.Open();

        _options = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlite(_connection)
            .Options;

        using var context = new AppDbContext(_options);
        context.Database.EnsureCreated();
    }

    [Fact]
    public async Task CreateUserPlan_ShouldPersistMinimalPlan()
    {
        await using var context = new AppDbContext(_options);
        var repository = new UserPlanRepository(context);
        var service = new UserPlanService(repository);

        var plan = await service.CreateAsync(new UserPlanCreateRequest(
            "Freedom Plan",
            "GBP"));

        Assert.NotEqual(Guid.Empty, plan.Id);
        Assert.Equal("Freedom Plan", plan.Name);
        Assert.Equal("GBP", plan.Currency);
        Assert.True(plan.CreatedDate <= DateTime.UtcNow);

        var saved = await context.UserPlans.SingleAsync();
        Assert.Equal(plan.Id, saved.Id);
        Assert.Equal("Freedom Plan", saved.Name);
        Assert.Equal("GBP", saved.Currency);
    }

    public void Dispose()
    {
        _connection.Dispose();
    }
}
