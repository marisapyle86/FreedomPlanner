namespace FreedomPlanner.Infrastructure.Data;

public sealed class UserPlanEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Currency { get; set; } = string.Empty;
    public DateTime CreatedDate { get; set; }

    public CashReserveEntity? CashReserve { get; set; }
}
