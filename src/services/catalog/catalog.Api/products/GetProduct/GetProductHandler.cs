using BuildingBlocks.CQRS;
using catalog.Api.Models;
using catalog.Api.Data;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace catalog.Api.products.GetProduct;

public record GetProductQuery() : IQuery<GetProductResult>;
public record GetProductResult(IEnumerable<Product> Products);

internal class GetProductHandler(CatalogContext context, ILogger<GetProductHandler> logger)
    : IQueryHandler<GetProductQuery, GetProductResult>
{
    private readonly CatalogContext _context = context;
    private readonly ILogger<GetProductHandler> _logger = logger;

    public async Task<GetProductResult> Handle(GetProductQuery query, CancellationToken cancellationToken)
    {
        _logger.LogInformation("GetProductQueryHandler.Handle llamado");

        var products = await Microsoft.EntityFrameworkCore.EntityFrameworkQueryableExtensions.ToListAsync(
            _context.Products,
            cancellationToken);

        return new GetProductResult(products);
    }
}