using FreedomPlanner.Domain.Entities;

namespace FreedomPlanner.Application.Interfaces;

public interface ICashReserveRepository
{
    Task<CashReserve> UpsertAsync(CashReserve reserve, CancellationToken cancellationToken = default);
    Task<CashReserve?> GetByUserPlanIdAsync(Guid userPlanId, CancellationToken cancellationToken = default);
}
