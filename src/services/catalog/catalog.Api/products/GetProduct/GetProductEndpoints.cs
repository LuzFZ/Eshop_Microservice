namespace catalog.Api.products.GetProduct; // Asegúrate que el namespace sea GetProduct

public record GetProductResponse(IEnumerable<Models.Product> Products);

public class GetProductEndpoints : Carter.ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products", async (ISender sender) =>
        {
            var result = await sender.Send(new GetProductQuery());
            return Results.Ok(result.Adapt<GetProductResponse>());
        })
        .WithName("GetProducts")
        .WithSummary("Get Products");
    }
}