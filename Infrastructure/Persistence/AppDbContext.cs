using Infrastructure.Persistence.Product;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

internal class AppDbContext : DbContext
{
    public DbSet<ProductEntity> Products => Set<ProductEntity>();

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(AppDbContext).Assembly);
    }
}