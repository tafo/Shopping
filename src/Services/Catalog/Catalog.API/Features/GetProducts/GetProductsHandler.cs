namespace Catalog.API.Features.GetProducts;

public record GetProductsQuery(int? PageNumber, int? PageSize = 10) : IQuery<GetProductsResult>;

public record GetProductsResult(IEnumerable<ProductEntity> Products);

public class GetProductsQueryHandler(IQuerySession session) : IQueryHandler<GetProductsQuery, GetProductsResult>
{
    public async Task<GetProductsResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
    {
        var products = await session.Query<ProductEntity>().ToPagedListAsync(query.PageNumber ?? 1, query.PageSize ?? 10, cancellationToken);

        return new GetProductsResult(products);
    }
}