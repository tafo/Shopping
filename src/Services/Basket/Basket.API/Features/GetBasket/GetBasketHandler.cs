namespace Basket.API.Features.GetBasket;

public record GetBasketResult(BasketEntity Basket);

public record GetBasketQuery(string Username) : IQuery<GetBasketResult>;

public class GetBasketQueryHandler(IBasketRepository repository) : IQueryHandler<GetBasketQuery, GetBasketResult>
{
    public async Task<GetBasketResult> Handle(GetBasketQuery query, CancellationToken cancellationToken)
    {
        var basket = await repository.GetBasket(query.Username, cancellationToken);
        return new GetBasketResult(basket);
    }
}