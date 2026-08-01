using FreedomPlanner.Application.Interfaces;
using FreedomPlanner.Application.Services;
using FreedomPlanner.Infrastructure.Data;
using FreedomPlanner.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FreedomPlanner.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? "Data Source=FreedomPlanner.db";

        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlite(connectionString));

        services.AddScoped<IUserPlanRepository, UserPlanRepository>();
        services.AddScoped<ICashReserveRepository, CashReserveRepository>();
        services.AddScoped<UserPlanService>();
        services.AddScoped<CashReserveService>();
        services.AddScoped<DashboardService>();

        return services;
    }
}
