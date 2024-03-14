namespace Basket.API.Features.GetBasket;

public record GetBasketResult(BasketEntity Basket);

public record GetBasketQuery(string UserName) : IQuery<GetBasketResult>;

public class GetBasketQueryHandler(IBasketRepository repository) : IQueryHandler<GetBasketQuery, GetBasketResult>
{
    public async Task<GetBasketResult> Handle(GetBasketQuery query, CancellationToken cancellationToken)
    {
        var basket = await repository.GetBasket(query.UserName, cancellationToken);
        return new GetBasketResult(basket);
    }
}