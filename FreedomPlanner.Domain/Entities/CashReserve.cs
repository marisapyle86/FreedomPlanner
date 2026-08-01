namespace FreedomPlanner.Domain.Entities;

public sealed class CashReserve
{
    public Guid Id { get; private set; }
    public Guid UserPlanId { get; private set; }
    public decimal CurrentBalance { get; private set; }
    public decimal TargetBalance { get; private set; }

    public CashReserve(Guid id, Guid userPlanId, decimal currentBalance, decimal targetBalance)
    {
        Id = id;
        UserPlanId = userPlanId;
        CurrentBalance = currentBalance;
        TargetBalance = targetBalance <= 0 ? 10000m : targetBalance;
    }
}
