namespace FreedomPlanner.Domain.Entities;

public sealed class UserPlan
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Currency { get; private set; } = string.Empty;
    public DateTime CreatedDate { get; private set; }

    public UserPlan(Guid id, string name, string currency, DateTime createdDate)
    {
        Id = id;
        Name = name;
        Currency = currency;
        CreatedDate = createdDate;
    }
}
