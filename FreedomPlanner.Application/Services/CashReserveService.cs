using System.Globalization;
using FreedomPlanner.Application.Interfaces;
using FreedomPlanner.Domain.Entities;

namespace FreedomPlanner.Application.Services;

public sealed class CashReserveService
{
    private readonly ICashReserveRepository? _repository;

    public CashReserveService()
    {
    }

    public CashReserveService(ICashReserveRepository repository)
    {
        _repository = repository;
    }

    public CashReserveSummaryResponse CalculateSummary(CashReserve reserve)
    {
        var targetBalance = reserve.TargetBalance <= 0 ? 10000m : reserve.TargetBalance;
        var completionPercentage = targetBalance == 0
            ? 100m
            : Math.Min(100m, (reserve.CurrentBalance / targetBalance) * 100m);

        var remainingAmount = Math.Max(0m, targetBalance - reserve.CurrentBalance);
        var status = completionPercentage switch
        {
            0m => CashReserveStatus.NotStarted,
            100m => CashReserveStatus.Complete,
            _ => CashReserveStatus.InProgress,
        };

        var recommendation = status == CashReserveStatus.Complete
            ? string.Empty
            : $"Build your cash reserve by saving {FormatCurrency(remainingAmount)} more.";

        var insight = completionPercentage switch
        {
            25m => "Milestone reached: 25% of your reserve target.",
            50m => "Milestone reached: 50% of your reserve target.",
            75m => "Milestone reached: 75% of your reserve target.",
            100m => "Milestone reached: 100% of your reserve target.",
            _ => null,
        };

        return new CashReserveSummaryResponse(
            completionPercentage,
            remainingAmount,
            status,
            recommendation,
            insight);
    }

    public async Task<CashReserveSummaryResponse> CreateAsync(Guid userPlanId, CashReserveCreateRequest request, CancellationToken cancellationToken = default)
    {
        if (_repository is null)
        {
            throw new InvalidOperationException("A repository instance is required to create a cash reserve.");
        }

        var currentBalance = request.CurrentBalance ?? 0m;
        var targetBalance = request.TargetBalance ?? 10000m;

        var reserve = new CashReserve(
            Guid.NewGuid(),
            userPlanId,
            currentBalance,
            targetBalance);

        await _repository.UpsertAsync(reserve, cancellationToken);

        return CalculateSummary(reserve);
    }

    public async Task<CashReserveSummaryResponse?> GetCurrentAsync(Guid userPlanId, CancellationToken cancellationToken = default)
    {
        if (_repository is null)
        {
            throw new InvalidOperationException("A repository instance is required to get a cash reserve.");
        }

        var reserve = await _repository.GetByUserPlanIdAsync(userPlanId, cancellationToken);

        return reserve is null
            ? null
            : CalculateSummary(reserve);
    }

    private static string FormatCurrency(decimal value)
    {
        return value.ToString("C0", CultureInfo.GetCultureInfo("en-GB"));
    }
}

public sealed record CashReserveCreateRequest(decimal? CurrentBalance, decimal? TargetBalance);

public sealed record CashReserveSummaryResponse(
    decimal CompletionPercentage,
    decimal RemainingAmount,
    CashReserveStatus Status,
    string Recommendation,
    string? Insight = null);

public enum CashReserveStatus
{
    NotStarted,
    InProgress,
    Complete
}
