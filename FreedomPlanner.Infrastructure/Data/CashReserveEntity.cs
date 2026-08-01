namespace FreedomPlanner.Infrastructure.Data;

public sealed class CashReserveEntity
{
    public Guid Id { get; set; }
    public Guid UserPlanId { get; set; }
    public decimal CurrentBalance { get; set; }
    public decimal TargetBalance { get; set; }

    public UserPlanEntity? UserPlan { get; set; }
}
