using FreedomPlanner.Application.Interfaces;
using FreedomPlanner.Domain.Entities;

namespace FreedomPlanner.Application.Services;

public sealed class DashboardService
{
    private readonly UserPlanService _userPlanService;
    private readonly CashReserveService _cashReserveService;

    public DashboardService(UserPlanService userPlanService, CashReserveService cashReserveService)
    {
        _userPlanService = userPlanService;
        _cashReserveService = cashReserveService;
    }

    public async Task<DashboardViewModel> GetCurrentAsync(CancellationToken cancellationToken = default)
    {
        var userPlan = await _userPlanService.GetCurrentAsync(cancellationToken);
        var reserveSummary = userPlan is null
            ? null
            : await _cashReserveService.GetCurrentAsync(userPlan.Id, cancellationToken);

        var recommendations = new List<string>();
        var insights = new List<string>();

        if (reserveSummary is not null)
        {
            if (!string.IsNullOrWhiteSpace(reserveSummary.Recommendation))
            {
                recommendations.Add(reserveSummary.Recommendation);
            }

            if (!string.IsNullOrWhiteSpace(reserveSummary.Insight))
            {
                insights.Add(reserveSummary.Insight);
            }
        }

        return new DashboardViewModel(
            DateTime.UtcNow,
            "1.0",
            userPlan,
            reserveSummary,
            recommendations,
            insights,
            new PlaceholderSection("Mortgage", "Coming soon", DashboardSectionStatus.Placeholder, true),
            new PlaceholderSection("Investments", "Coming soon", DashboardSectionStatus.Placeholder, true),
            new PlaceholderSection("Freedom Ladder", "Coming soon", DashboardSectionStatus.Placeholder, true));
    }
}

public sealed record DashboardViewModel(
    DateTime GeneratedAtUtc,
    string AssumptionVersion,
    UserPlanResponse? UserPlan,
    CashReserveSummaryResponse? CashReserve,
    List<string> Recommendations,
    List<string> Insights,
    PlaceholderSection Mortgage,
    PlaceholderSection Investments,
    PlaceholderSection FreedomLadder);

public sealed record PlaceholderSection(
    string Title,
    string Description,
    DashboardSectionStatus Status,
    bool IsPlaceholder);

public enum DashboardSectionStatus
{
    Placeholder,
    Ready
}
