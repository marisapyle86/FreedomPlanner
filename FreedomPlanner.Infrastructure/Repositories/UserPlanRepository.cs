using FreedomPlanner.Application.Interfaces;
using FreedomPlanner.Domain.Entities;
using FreedomPlanner.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FreedomPlanner.Infrastructure.Repositories;

public sealed class UserPlanRepository : IUserPlanRepository
{
    private readonly AppDbContext _context;

    public UserPlanRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<UserPlan> AddAsync(UserPlan userPlan, CancellationToken cancellationToken = default)
    {
        var entity = new UserPlanEntity
        {
            Id = userPlan.Id,
            Name = userPlan.Name,
            Currency = userPlan.Currency,
            CreatedDate = userPlan.CreatedDate
        };

        _context.UserPlans.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);

        return userPlan;
    }

    public async Task<UserPlan?> GetCurrentAsync(CancellationToken cancellationToken = default)
    {
        var entity = await _context.UserPlans
            .OrderByDescending(x => x.CreatedDate)
            .FirstOrDefaultAsync(cancellationToken);

        if (entity is null)
        {
            return null;
        }

        return new UserPlan(entity.Id, entity.Name, entity.Currency, entity.CreatedDate);
    }
}
