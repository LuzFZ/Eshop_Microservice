using catalog.Api.Data;
using Microsoft.EntityFrameworkCore;
using Carter;


internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        
        var connectionString = builder.Configuration.GetConnectionString("Database");

        
        builder.Services.AddCarter();
        builder.Services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(typeof(Program).Assembly);
        });

        
        builder.Services.AddDbContext<CatalogContext>(options =>
            options.UseSqlServer(connectionString));



        var app = builder.Build();


        app.MapCarter();

        
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

        
        app.Run();
    }
}