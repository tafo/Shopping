using Discount.Grpc;

namespace Basket.API.Features.StoreBasket;

public record StoreBasketCommand(BasketEntity Basket) : ICommand<StoreBasketResult>;
public record StoreBasketResult(string UserName);

public class StoreBasketCommandValidator : AbstractValidator<StoreBasketCommand>
{
    public StoreBasketCommandValidator()
    {
        RuleFor(x => x.Basket).NotNull().WithMessage("Basket can not be null");
        RuleFor(x => x.Basket.Username).NotEmpty().WithMessage("UserName is required");
    }
}

public class StoreBasketCommandHandler(IBasketRepository repository, DiscountProtoService.DiscountProtoServiceClient discountClient) : ICommandHandler<StoreBasketCommand, StoreBasketResult>
{
    public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
    {
        foreach (var item in command.Basket.ItemList)
        {
            var coupon = await discountClient.GetDiscountAsync(new GetDiscountRequest { ProductName = item.ProductName }, cancellationToken: cancellationToken);
            item.Price -= coupon.Amount;
        }
        
        await repository.StoreBasket(command.Basket, cancellationToken);
        return new StoreBasketResult(command.Basket.Username);
    }
}