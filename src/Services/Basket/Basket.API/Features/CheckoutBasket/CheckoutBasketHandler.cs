using Basket.API.Dtos;
using Common.Messaging.Events;
using MassTransit;

namespace Basket.API.Features.CheckoutBasket;

public record CheckoutBasketCommand(BasketCheckoutDto BasketCheckoutDto) : ICommand<CheckoutBasketResult>;
public record CheckoutBasketResult(bool IsSuccess);

public class CheckoutBasketCommandValidator : AbstractValidator<CheckoutBasketCommand>
{
    public CheckoutBasketCommandValidator()
    {
        RuleFor(x => x.BasketCheckoutDto).NotNull().WithMessage("BasketCheckoutDto can not be null");
        RuleFor(x => x.BasketCheckoutDto.Username).NotEmpty().WithMessage("Username is required");
    }
}

public class CheckoutBasketCommandHandler(IBasketRepository repository, IPublishEndpoint publishEndpoint) : ICommandHandler<CheckoutBasketCommand, CheckoutBasketResult>
{
    public async Task<CheckoutBasketResult> Handle(CheckoutBasketCommand command, CancellationToken cancellationToken)
    {
        var basket = await repository.GetBasket(command.BasketCheckoutDto.Username, cancellationToken);
        var eventMessage = command.BasketCheckoutDto.Adapt<BasketCheckoutEvent>();
        eventMessage.TotalPrice = basket.TotalPrice;
        await publishEndpoint.Publish(eventMessage, cancellationToken);
        await repository.DeleteBasket(command.BasketCheckoutDto.Username, cancellationToken);

        return new CheckoutBasketResult(true);
    }
}