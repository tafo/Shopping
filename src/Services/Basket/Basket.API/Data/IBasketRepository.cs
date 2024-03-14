namespace Basket.API.Data;

public interface IBasketRepository
{
    Task<BasketEntity> GetBasket(string userName, CancellationToken cancellationToken = default);
    Task<BasketEntity> StoreBasket(BasketEntity basket, CancellationToken cancellationToken = default);
    Task<bool> DeleteBasket(string userName, CancellationToken cancellationToken = default);
}