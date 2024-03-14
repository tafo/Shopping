namespace Catalog.API.Features.GetProductByCategory;

public record GetProductByCategoryResult(IEnumerable<ProductEntity> ProductList);

public record GetProductByCategoryQuery(string Category) : IQuery<GetProductByCategoryResult>;

internal class GetProductByCategoryQueryHandler(IDocumentSession session) : IQueryHandler<GetProductByCategoryQuery, GetProductByCategoryResult>
{
    public async Task<GetProductByCategoryResult> Handle(GetProductByCategoryQuery query, CancellationToken cancellationToken)
    {
        var products = await session.Query<ProductEntity>().Where(x => x.CategoryList.Contains(query.Category)).ToListAsync(cancellationToken);
        return new GetProductByCategoryResult(products);
    }
}