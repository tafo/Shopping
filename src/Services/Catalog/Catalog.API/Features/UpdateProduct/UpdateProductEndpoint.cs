namespace Catalog.API.Features.UpdateProduct;

public record UpdateProductResponse(bool IsSuccess);

public record UpdateProductRequest(
    Guid Id,
    string Name,
    List<string> CategoryList,
    string Description,
    string ImageFile,
    decimal Price);

public class UpdateProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/products", async (UpdateProductRequest request, ISender sender) =>
            {
                var command = request.Adapt<UpdateProductCommand>();
                var result = await sender.Send(command);
                var response = result.Adapt<UpdateProductResult>();
                return Results.Ok(response);
            }).WithName("UpdateProduct")
            .Produces<UpdateProductResponse>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Update Product")
            .WithDescription("Update Product");
    }
}