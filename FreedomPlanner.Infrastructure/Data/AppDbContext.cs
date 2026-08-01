using Microsoft.EntityFrameworkCore;

namespace FreedomPlanner.Infrastructure.Data;

public sealed class AppDbContext : DbContext
{
    public DbSet<UserPlanEntity> UserPlans => Set<UserPlanEntity>();
    public DbSet<CashReserveEntity> CashReserves => Set<CashReserveEntity>();

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserPlanEntity>(entity =>
        {
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(200);

            entity.Property(x => x.Currency)
                .IsRequired()
                .HasMaxLength(3);

            entity.Property(x => x.CreatedDate)
                .IsRequired();
        });

        modelBuilder.Entity<CashReserveEntity>(entity =>
        {
            entity.HasKey(x => x.Id);
            entity.Property(x => x.CurrentBalance).HasColumnType("decimal(18,2)");
            entity.Property(x => x.TargetBalance).HasColumnType("decimal(18,2)");
            entity.HasOne(x => x.UserPlan)
                .WithOne(x => x.CashReserve)
                .HasForeignKey<CashReserveEntity>(x => x.UserPlanId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        base.OnModelCreating(modelBuilder);
    }
}
