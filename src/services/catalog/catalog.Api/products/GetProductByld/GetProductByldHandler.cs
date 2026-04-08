using BuildingBlocks.CQRS;
using catalog.Api.Models;
using catalog.Api.Data;
using catalog.Api.Exeptios; 
using Microsoft.EntityFrameworkCore; 
using Microsoft.Extensions.Logging;

namespace catalog.Api.products.GetProductByld;

public record GetProductByIdQuery(Guid Id) : IQuery<GetProductByIdResult>;

public record GetProductByIdResult(Product Product);

internal class GetProductByQueryHandler
    (CatalogContext context, ILogger<GetProductByQueryHandler> logger)
    : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
{
    private readonly ILogger<GetProductByQueryHandler> logger = logger;

    public async Task<GetProductByIdResult> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        
        var product = await Microsoft.EntityFrameworkCore.EntityFrameworkQueryableExtensions
    .FirstOrDefaultAsync(context.Products, p => p.Id == request.Id, cancellationToken);

        
        if (product is null)
        {
            throw new ProductNotFoundExeption();
        }

        
        return new GetProductByIdResult(product);
    }
}