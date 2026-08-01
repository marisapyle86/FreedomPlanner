using FreedomPlanner.Domain.Entities;

namespace FreedomPlanner.Application.Interfaces;

public interface IUserPlanRepository
{
    Task<UserPlan> AddAsync(UserPlan userPlan, CancellationToken cancellationToken = default);
    Task<UserPlan?> GetCurrentAsync(CancellationToken cancellationToken = default);
}
