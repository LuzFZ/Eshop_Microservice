using Microsoft.EntityFrameworkCore;
using catalog.Api.Models;

namespace catalog.Api.Data;

public class CatalogContext : DbContext
{
    public CatalogContext(DbContextOptions<CatalogContext> options) : base(options)
    {
    }

    public DbSet<Product> Products => Set<Product>();

    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Product>(entity =>
        {
            
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnType("uniqueidentifier");

            entity.Property(e => e.Price).HasColumnType("decimal(18,2)");
        });
    }
}