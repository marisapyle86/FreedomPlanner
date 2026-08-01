using FreedomPlanner.Application.Interfaces;
using FreedomPlanner.Domain.Entities;

namespace FreedomPlanner.Application.Services;

public sealed class UserPlanService
{
    private readonly IUserPlanRepository _repository;

    public UserPlanService(IUserPlanRepository repository)
    {
        _repository = repository;
    }

    public async Task<UserPlanResponse> CreateAsync(UserPlanCreateRequest request, CancellationToken cancellationToken = default)
    {
        var name = request.Name?.Trim() ?? string.Empty;
        var currency = request.Currency?.Trim().ToUpperInvariant() ?? string.Empty;

        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("A plan name is required.", nameof(request.Name));
        }

        if (string.IsNullOrWhiteSpace(currency))
        {
            throw new ArgumentException("A currency code is required.", nameof(request.Currency));
        }

        var plan = new UserPlan(
            Guid.NewGuid(),
            name,
            currency,
            DateTime.UtcNow);

        var createdPlan = await _repository.AddAsync(plan, cancellationToken);

        return new UserPlanResponse(
            createdPlan.Id,
            createdPlan.Name,
            createdPlan.Currency,
            createdPlan.CreatedDate);
    }

    public async Task<UserPlanResponse?> GetCurrentAsync(CancellationToken cancellationToken = default)
    {
        var userPlan = await _repository.GetCurrentAsync(cancellationToken);

        if (userPlan is null)
        {
            return null;
        }

        return new UserPlanResponse(
            userPlan.Id,
            userPlan.Name,
            userPlan.Currency,
            userPlan.CreatedDate);
    }
}

public sealed record UserPlanCreateRequest(string? Name, string? Currency);

public sealed record UserPlanResponse(Guid Id, string Name, string Currency, DateTime CreatedDate);
