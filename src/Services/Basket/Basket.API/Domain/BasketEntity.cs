namespace Basket.API.Domain;

public class BasketEntity
{
    public string Username { get; set; } = default!;
    public List<BasketItemEntity> ItemList { get; set; } = [];
    public decimal TotalPrice => ItemList.Sum(x => x.Price * x.Quantity);

    public BasketEntity(string username)
    {
        Username = username;
    }

    public BasketEntity()
    {
    }
}