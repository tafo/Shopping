namespace Catalog.API.Features.GetProducts;

public record GetProductResponse(IEnumerable<ProductEntity> Products);

public class GetProductsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products", async (ISender sender) =>
        {
            var result = await sender.Send(new GetProductsQuery());
            var response = result.Adapt<GetProductResponse>();
            return Results.Ok(response);
        })
        .WithName("GetProduct")
        .Produces<GetProductResponse>()
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Products")
        .WithDescription("Get Products");
    }
}