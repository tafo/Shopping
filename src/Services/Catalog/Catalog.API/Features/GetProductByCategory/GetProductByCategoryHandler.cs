namespace Catalog.API.Features.GetProductByCategory;

public record GetProductByCategoryResult(IEnumerable<ProductEntity> ProductList);

public record GetProductByCategoryQuery(string Category) : IQuery<GetProductByCategoryResult>;

internal class GetProductByCategoryQueryHandler(IDocumentSession session, ILogger<GetProductByCategoryQueryHandler> logger) : IQueryHandler<GetProductByCategoryQuery, GetProductByCategoryResult>
{
    public async Task<GetProductByCategoryResult> Handle(GetProductByCategoryQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetProductByCategoryHandler.Handle called with {@Query}", query);
        var products = await session.Query<ProductEntity>().Where(x => x.CategoryList.Contains(query.Category)).ToListAsync(cancellationToken);
        return new GetProductByCategoryResult(products);
    }
}