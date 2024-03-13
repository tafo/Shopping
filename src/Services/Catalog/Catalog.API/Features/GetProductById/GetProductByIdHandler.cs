using Catalog.API.Features.GetProducts;

namespace Catalog.API.Features.GetProductById;

public record GetProductByIdResult(ProductEntity Product);

public record GetProductByIdQuery(Guid Id) : IQuery<GetProductByIdResult>;

internal class GetProductByIdQueryHandler(IDocumentSession session, ILogger<GetProductByIdQueryHandler> logger) : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
{
    public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetProductByIdQueryHandler.Handle called with {@Query}", query);

        var product = await session.LoadAsync<ProductEntity>(query.Id, cancellationToken);

        if (product is null)
        {
            throw new ProductNotFoundException();
        }

        return new GetProductByIdResult(product);
    }
}