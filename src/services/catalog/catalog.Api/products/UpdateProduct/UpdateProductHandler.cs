using BuildingBlocks.CQRS;
using catalog.Api.Data;
using catalog.Api.Models;
using catalog.Api.Exeptios; 
using Microsoft.EntityFrameworkCore;

namespace catalog.Api.products.UpdateProduct;


public record UpdateProductCommand(Guid Id, string Name, List<string> Category, string Description, string ImageFile, decimal Price)
    : ICommand<UpdateProductResult>;

public record UpdateProductResult(bool IsSuccess);

internal class UpdateProductCommandHandler
    (CatalogContext context, ILogger<UpdateProductCommandHandler> logger)
    : ICommandHandler<UpdateProductCommand, UpdateProductResult>
{
    public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("UpdateProductHandler.Handle called with {@Command}", command);


        var product = await Microsoft.EntityFrameworkCore.EntityFrameworkQueryableExtensions
        .FirstOrDefaultAsync(context.Products, p => p.Id == command.Id, cancellationToken);

        if (product == null)
        {
            throw new ProductNotFoundExeption();
        }

        
        product.Name = command.Name;
        product.Category = command.Category;
        product.Description = command.Description;
        product.ImageFile = command.ImageFile;
        product.Price = command.Price;

        // Guardamos los cambios en SQL
        await context.SaveChangesAsync(cancellationToken);

        return new UpdateProductResult(true);
    }
}