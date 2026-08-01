using FreedomPlanner.Application.Interfaces;
using FreedomPlanner.Domain.Entities;
using FreedomPlanner.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FreedomPlanner.Infrastructure.Repositories;

public sealed class CashReserveRepository : ICashReserveRepository
{
    private readonly AppDbContext _context;

    public CashReserveRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<CashReserve> UpsertAsync(CashReserve reserve, CancellationToken cancellationToken = default)
    {
        var entity = await _context.CashReserves
            .SingleOrDefaultAsync(x => x.UserPlanId == reserve.UserPlanId, cancellationToken);

        if (entity is null)
        {
            entity = new CashReserveEntity
            {
                Id = reserve.Id,
                UserPlanId = reserve.UserPlanId,
                CurrentBalance = reserve.CurrentBalance,
                TargetBalance = reserve.TargetBalance
            };

            _context.CashReserves.Add(entity);
        }
        else
        {
            entity.CurrentBalance = reserve.CurrentBalance;
            entity.TargetBalance = reserve.TargetBalance;
        }

        await _context.SaveChangesAsync(cancellationToken);

        return reserve;
    }

    public async Task<CashReserve?> GetByUserPlanIdAsync(Guid userPlanId, CancellationToken cancellationToken = default)
    {
        var entity = await _context.CashReserves
            .SingleOrDefaultAsync(x => x.UserPlanId == userPlanId, cancellationToken);

        if (entity is null)
        {
            return null;
        }

        return new CashReserve(
            entity.Id,
            entity.UserPlanId,
            entity.CurrentBalance,
            entity.TargetBalance);
    }
}
