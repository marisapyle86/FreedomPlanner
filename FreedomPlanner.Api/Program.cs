using System.Text.Json.Serialization;
using FreedomPlanner.Application.Services;
using FreedomPlanner.Infrastructure;
using FreedomPlanner.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
});
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/api/health", () => Results.Ok(new { status = "Healthy" }));

app.MapGet("/api/dashboard", async (DashboardService service, CancellationToken cancellationToken) =>
{
    var result = await service.GetCurrentAsync(cancellationToken);
    return Results.Ok(result);
});

app.MapPost("/api/user-plan", async (UserPlanCreateRequest request, UserPlanService service, CancellationToken cancellationToken) =>
{
    try
    {
        var result = await service.CreateAsync(request, cancellationToken);
        return Results.Created("/api/user-plan", result);
    }
    catch (ArgumentException ex)
    {
        return Results.BadRequest(ex.Message);
    }
});

app.MapGet("/api/user-plan", async (UserPlanService service, CancellationToken cancellationToken) =>
{
    var result = await service.GetCurrentAsync(cancellationToken);
    return result is null ? Results.NotFound() : Results.Ok(result);
});

app.MapPost("/api/user-plan/{userPlanId:guid}/cash-reserve", async (Guid userPlanId, CashReserveCreateRequest request, CashReserveService service, CancellationToken cancellationToken) =>
{
    var result = await service.CreateAsync(userPlanId, request, cancellationToken);
    return Results.Ok(result);
});

app.MapGet("/api/user-plan/{userPlanId:guid}/cash-reserve", async (Guid userPlanId, CashReserveService service, CancellationToken cancellationToken) =>
{
    var result = await service.GetCurrentAsync(userPlanId, cancellationToken);
    return result is null ? Results.NotFound() : Results.Ok(result);
});

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.EnsureDeleted();
    dbContext.Database.EnsureCreated();
}

app.Run();

public partial class Program { }
