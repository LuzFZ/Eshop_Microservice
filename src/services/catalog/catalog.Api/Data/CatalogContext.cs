using Microsoft.EntityFrameworkCore;

namespace catalog.Api.Data;


public class CatalogContext : DbContext
{
    public CatalogContext(DbContextOptions<CatalogContext> options) : base(options)
    {
    }

    public object Products { get; internal set; }
}