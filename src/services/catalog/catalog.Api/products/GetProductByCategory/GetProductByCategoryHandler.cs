using BuildingBlocks.CQRS;
using catalog.Api.Models;
using Microsoft.EntityFrameworkCore;
using catalog.Api.Data;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace catalog.Api.products.GetProductByCategory;

public record GetProductByCategoryQuery(string Category) : IQuery<GetProductByCategoryResult>;
public record GetProductByCategoryResult(IEnumerable<Product> Products);

internal class GetProductByCategoryHandler
    (CatalogContext context, ILogger<GetProductByCategoryHandler> logger)
    : IQueryHandler<GetProductByCategoryQuery, GetProductByCategoryResult>
{
    public async Task<GetProductByCategoryResult> Handle(GetProductByCategoryQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetProductByCategoryHandler.Handle called with {@Query}", query);

        var products = await Microsoft.EntityFrameworkCore.EntityFrameworkQueryableExtensions
            .ToListAsync(context.Products.Where(p => p.Category.Contains(query.Category)), cancellationToken);

        return new GetProductByCategoryResult(products);
    }
}