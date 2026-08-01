using FreedomPlanner.Application.Interfaces;
using FreedomPlanner.Application.Services;
using FreedomPlanner.Domain.Entities;
using Xunit;

namespace FreedomPlanner.Tests;

public sealed class DashboardServiceTests
{
    [Fact]
    public async Task GetCurrentAsync_ShouldAggregateCurrentUserPlanAndReserveState()
    {
        var service = new DashboardService(new UserPlanService(new StubUserPlanRepository()), new CashReserveService(new StubCashReserveRepository()));

        var dashboard = await service.GetCurrentAsync();

        Assert.NotNull(dashboard.UserPlan);
        Assert.NotNull(dashboard.CashReserve);
        Assert.Contains("Build your cash reserve by saving £7,500 more.", dashboard.Recommendations);
        Assert.Contains("Milestone reached: 25% of your reserve target.", dashboard.Insights);
        Assert.Equal("Mortgage", dashboard.Mortgage.Title);
        Assert.Equal("Investments", dashboard.Investments.Title);
        Assert.Equal("Freedom Ladder", dashboard.FreedomLadder.Title);
    }

    [Fact]
    public async Task GetCurrentAsync_ShouldExposeDashboardMetadataAndStableSectionContract()
    {
        var service = new DashboardService(new UserPlanService(new StubUserPlanRepository()), new CashReserveService(new StubCashReserveRepository()));

        var dashboard = await service.GetCurrentAsync();

        Assert.True(dashboard.GeneratedAtUtc <= DateTime.UtcNow);
        Assert.Equal("1.0", dashboard.AssumptionVersion);
        Assert.Equal("Mortgage", dashboard.Mortgage.Title);
        Assert.Equal("Investments", dashboard.Investments.Title);
        Assert.Equal("Freedom Ladder", dashboard.FreedomLadder.Title);
        Assert.Equal(DashboardSectionStatus.Placeholder, dashboard.Mortgage.Status);
        Assert.True(dashboard.Mortgage.IsPlaceholder);
    }

    private sealed class StubUserPlanRepository : IUserPlanRepository
    {
        public Task<UserPlan> AddAsync(UserPlan userPlan, CancellationToken cancellationToken = default)
            => Task.FromResult(userPlan);

        public Task<UserPlan?> GetCurrentAsync(CancellationToken cancellationToken = default)
            => Task.FromResult<UserPlan?>(new UserPlan(Guid.NewGuid(), "Freedom Plan", "GBP", DateTime.UtcNow));
    }

    private sealed class StubCashReserveRepository : ICashReserveRepository
    {
        public Task<CashReserve> UpsertAsync(CashReserve reserve, CancellationToken cancellationToken = default)
            => Task.FromResult(reserve);

        public Task<CashReserve?> GetByUserPlanIdAsync(Guid userPlanId, CancellationToken cancellationToken = default)
            => Task.FromResult<CashReserve?>(new CashReserve(Guid.NewGuid(), userPlanId, 2500m, 10000m));
    }
}
