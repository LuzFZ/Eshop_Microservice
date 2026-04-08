using Carter;
using Mapster;
using MediatR;
using catalog.Api.Models;

namespace catalog.Api.products.GetProductByCategory;

public record GetProductByCategoryResponse(IEnumerable<Product> Products);

public class GetProductyCategoryEndpoint : ICarterModule
{
    // 1. Corregido: IEndpointRouteBuilder (sin la 's' extra y sin 'Routes')
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        // 2. Corregido: Agregada la coma antes del async
        app.MapGet("/products/category/{category}",
            async (string category, ISender sender) =>
            {
                var result = await sender.Send(new GetProductByCategoryQuery(category));

                // 3. Corregido: Es .Adapt con una sola 'd' (de la librería Mapster)
                var response = result.Adapt<GetProductByCategoryResponse>();

                return Results.Ok(response);
            })
            .WithName("GetProductByCategoryId")
            .Produces<GetProductByCategoryResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Product By Category")
            .WithDescription("Get Product By Category");
    }
}