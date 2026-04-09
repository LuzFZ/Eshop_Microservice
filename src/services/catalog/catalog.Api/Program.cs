using catalog.Api.Data;
using Microsoft.EntityFrameworkCore;
using Carter;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

// Servicios
builder.Services.AddCarter();
builder.Services.AddMediatR(config => {
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
});
builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);
builder.Services.AddDbContext<CatalogContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Database")));

var app = builder.Build();

app.MapCarter();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<CatalogContext>();
    context.Database.EnsureCreated();
}

app.Run();