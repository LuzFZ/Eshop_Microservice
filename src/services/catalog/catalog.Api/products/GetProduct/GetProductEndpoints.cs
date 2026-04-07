


namespace catalog.Api.Products.GetProducts;

// public record 

public record GetProductsResponse(IEnumerable<Product> Products);

public class GetProductEndpoints : ICarterModule

{
    public async void AddRoutes(IEndpointRouteBuilder app, object sender)
    {
        app.MapGet("/Products", async (ISender sender) =>

        {
            var result = await sender.Send(new GetProductsQuery());

            var response = result.Adapt<GetProductsResponse>();

            return Results.Ok(response);
        })
            .WithName("GetProducts")
            .Produces<GetProductsResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Products")
            .WithDescription("Get Products");
    }

    public void AddRoutes(IEndpointRouteBuilder app)
    {
        throw new NotImplementedException();
    }

    internal class GetProductsQuery : IRequest<object>
    {
    }
}
