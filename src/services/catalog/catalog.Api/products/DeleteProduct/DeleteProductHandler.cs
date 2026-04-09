using BuildingBlocks.CQRS;
using catalog.Api.Data;
using catalog.Api.Exeptios;
using Microsoft.EntityFrameworkCore;

namespace catalog.Api.products.DeleteProduct;

public record DeleteProductCommand(Guid Id) : ICommand<DeleteProductResult>;
public record DeleteProductResult(bool IsSuccess);

internal class DeleteProductCommandHandler
    (CatalogContext context, ILogger<DeleteProductCommandHandler> logger)
    : ICommandHandler<DeleteProductCommand, DeleteProductResult>
{
    public async Task<DeleteProductResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("DeleteProductHandler.Handle called with {@Command}", command);

        //  producto en SQL Server

        var product = await Microsoft.EntityFrameworkCore.EntityFrameworkQueryableExtensions
       .FirstOrDefaultAsync(context.Products, p => p.Id == command.Id, cancellationToken);

        if (product is null)
        {
            throw new ProductNotFoundExeption();
        }

        
        context.Products.Remove(product);
        await context.SaveChangesAsync(cancellationToken);

        return new DeleteProductResult(true);
    }
}