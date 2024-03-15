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

public class StoreBasketCommandHandler(IBasketRepository repository) : ICommandHandler<StoreBasketCommand, StoreBasketResult>
{
    public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
    {
        await repository.StoreBasket(command.Basket, cancellationToken);
        return new StoreBasketResult(command.Basket.Username);
    }
}