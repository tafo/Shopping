namespace Catalog.API.Features.DeleteProduct;

public record DeleteProductResult(bool IsSuccess);

public record DeleteProductCommand(Guid Id) : ICommand<DeleteProductResult>;

public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
{
    public DeleteProductCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required");
    }
}

public class DeleteProductCommandHandler(IDocumentSession session) : ICommandHandler<DeleteProductCommand, DeleteProductResult>
{
    public async Task<DeleteProductResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
    {
        session.Delete<ProductEntity>(command.Id);
        
        await session.SaveChangesAsync(cancellationToken);

        return new DeleteProductResult(true);
    }
}