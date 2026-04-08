using BuildingBlocks.CQRS;
using catalog.Api.Data;
using catalog.Api.Models;

namespace catalog.Api.products.createProduct;

public record CreateProductCommand(string Name, List<string> Category, string Description, string ImageFile, decimal Price)
    : ICommand<CreateProductResult>;

public record CreateProductResult(Guid Id);

internal class CreateProductHandler : ICommandHandler<CreateProductCommand, CreateProductResult>
{
    // Declaramos la variable manualmente
    private readonly CatalogContext _context;

    // Constructor tradicional
    public CreateProductHandler(CatalogContext context)
    {
        _context = context;
    }

    public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        var product = new Product
        {
            Name = command.Name,
            Category = command.Category,
            Description = command.Description,
            ImageFile = command.ImageFile,
            Price = command.Price
        };

        // Usamos _context con el guion bajo
        _context.Products.Add(product);
        await _context.SaveChangesAsync(cancellationToken);

        return new CreateProductResult(product.Id);
    }
}