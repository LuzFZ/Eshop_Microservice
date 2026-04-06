using catalog.Api;
using Carter;
using Microsoft.EntityFrameworkCore;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // 1. Obtener la conexión del appsettings.json
        var connectionString = builder.Configuration.GetConnectionString("Database");

        
        builder.Services.AddCarter();
        builder.Services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(typeof(Program).Assembly);
        });

        // 3. Configurar el Contexto de la BD
        builder.Services.AddDbContext<CatalogContext>(options =>
            options.UseSqlServer(connectionString));

        var app = builder.Build();

        
        app.MapCarter();

        app.Run();
    }
}