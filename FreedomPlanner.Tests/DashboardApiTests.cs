using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using FreedomPlanner.Application.Services;
using FreedomPlanner.Domain.Entities;
using FreedomPlanner.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace FreedomPlanner.Tests;

public sealed class DashboardApiTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public DashboardApiTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task GetDashboard_ShouldReturnDashboardPayload()
    {
        using var scope = _factory.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        dbContext.UserPlans.RemoveRange(dbContext.UserPlans);
        dbContext.CashReserves.RemoveRange(dbContext.CashReserves);
        await dbContext.SaveChangesAsync();

        var plan = new UserPlan(Guid.NewGuid(), "Freedom Plan", "GBP", DateTime.UtcNow);
        dbContext.UserPlans.Add(new UserPlanEntity
        {
            Id = plan.Id,
            Name = plan.Name,
            Currency = plan.Currency,
            CreatedDate = plan.CreatedDate
        });

        dbContext.CashReserves.Add(new CashReserveEntity
        {
            Id = Guid.NewGuid(),
            UserPlanId = plan.Id,
            CurrentBalance = 2500m,
            TargetBalance = 10000m
        });

        await dbContext.SaveChangesAsync();

        using var client = _factory.CreateClient();
        var response = await client.GetAsync("/api/dashboard");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var payload = await response.Content.ReadFromJsonAsync<DashboardViewModel>(new JsonSerializerOptions(JsonSerializerDefaults.Web)
        {
            Converters = { new JsonStringEnumConverter() }
        });

        Assert.NotNull(payload);
        Assert.NotNull(payload.UserPlan);
        Assert.NotNull(payload.CashReserve);
        Assert.Equal("1.0", payload.AssumptionVersion);
        Assert.Equal("Mortgage", payload.Mortgage.Title);
        Assert.Equal("Investments", payload.Investments.Title);
        Assert.Equal("Freedom Ladder", payload.FreedomLadder.Title);
    }
}
