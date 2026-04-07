using BuildingBlocks.CQRS;
using catalog.Api.Models;
using Microsoft.Extensions.Logging; 
using Marten;
using Microsoft.EntityFrameworkCore;

public record GetProductQuery() : IQuery<GetProductResult>;
public record GetProductResult(IEnumerable<Product> Products);

internal class GetProductHandler(IDocumentSession session, ILogger<GetProductHandler> logger)
    : IQueryHandler<GetProductQuery, GetProductResult>
{

    private readonly IDocumentSession _session = session;
    private readonly ILogger<GetProductHandler> _logger = logger;

    public IDocumentSession Session => _session;

    

    public async Task<GetProductResult> Handle(GetProductQuery query, CancellationToken cancellationToken)
    {
        _logger.LogInformation("GetProductQueryHandler.Handle called with {@Query}", query);

        var products = await Marten.QueryableExtensions.ToListAsync(session.Query<Product>(), cancellationToken);

        return new GetProductResult(products);
    }
}


