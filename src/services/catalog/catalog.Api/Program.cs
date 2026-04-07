using catalog.Api.Data;
using Microsoft.EntityFrameworkCore;
using Carter;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // 1. Obtener la conexión
        var connectionString = builder.Configuration.GetConnectionString("Database");

        // 2. Registrar Servicios
        builder.Services.AddCarter();
        builder.Services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(typeof(Program).Assembly);
        });

        // 3. Configurar el Contexto de la BD (SQL Server)
        builder.Services.AddDbContext<CatalogContext>(options =>
            options.UseSqlServer(connectionString));



        var app = builder.Build();


        app.MapCarter();

        // 5. Bloque de autogestión para crear la DB en Docker
        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            try
            {
                var context = services.GetRequiredService<CatalogContext>();
                context.Database.EnsureCreated();
                Console.WriteLine("Base de datos lista en Docker.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($" Error al conectar: {ex.Message}");
            }
        }

        // 6. Arrancar la aplicación
        app.Run();
    }
}