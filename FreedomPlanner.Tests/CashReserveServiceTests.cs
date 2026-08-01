using FreedomPlanner.Application.Services;
using FreedomPlanner.Domain.Entities;
using Xunit;

namespace FreedomPlanner.Tests;

public sealed class CashReserveServiceTests
{
    [Fact]
    public void CalculateSummary_ShouldCapCompletionAt100Percent()
    {
        var service = new CashReserveService();
        var summary = service.CalculateSummary(new CashReserve(
            Guid.NewGuid(),
            Guid.NewGuid(),
            15000m,
            10000m));

        Assert.Equal(100m, summary.CompletionPercentage);
        Assert.Equal(0m, summary.RemainingAmount);
        Assert.Equal(CashReserveStatus.Complete, summary.Status);
    }

    [Fact]
    public void CalculateSummary_ShouldReturnRecommendationAndMilestoneInsight()
    {
        var service = new CashReserveService();
        var summary = service.CalculateSummary(new CashReserve(
            Guid.NewGuid(),
            Guid.NewGuid(),
            2500m,
            10000m));

        Assert.Equal(25m, summary.CompletionPercentage);
        Assert.Equal(7500m, summary.RemainingAmount);
        Assert.Equal(CashReserveStatus.InProgress, summary.Status);
        Assert.Equal("Build your cash reserve by saving £7,500 more.", summary.Recommendation);
        Assert.Equal("Milestone reached: 25% of your reserve target.", summary.Insight);
    }

    [Fact]
    public void CalculateSummary_ShouldReportNotStartedWhenBalanceIsZero()
    {
        var service = new CashReserveService();
        var summary = service.CalculateSummary(new CashReserve(
            Guid.NewGuid(),
            Guid.NewGuid(),
            0m,
            10000m));

        Assert.Equal(0m, summary.CompletionPercentage);
        Assert.Equal(10000m, summary.RemainingAmount);
        Assert.Equal(CashReserveStatus.NotStarted, summary.Status);
    }
}
