using Microsoft.EntityFrameworkCore;
using catalog.Api.Models;

namespace catalog.Api.Data;

public class CatalogContext : DbContext
{
    public CatalogContext(DbContextOptions<CatalogContext> options) : base(options)
    {
    }

    public DbSet<Product> Products => Set<Product>();

    // AQUÍ ES DONDE VA LA CORRECCIÓN
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Product>(entity =>
        {
            // Forzamos a que el Id sea el tipo correcto en SQL Server
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnType("uniqueidentifier");

            // También corregimos el aviso del precio de paso
            entity.Property(e => e.Price).HasColumnType("decimal(18,2)");
        });
    }
}