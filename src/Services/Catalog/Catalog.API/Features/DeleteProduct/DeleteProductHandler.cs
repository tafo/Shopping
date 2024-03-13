namespace Catalog.API.Features.DeleteProduct;

public record DeleteProductResult(bool IsSuccess);

public record DeleteProductCommand(Guid Id) : ICommand<DeleteProductResult>;

public class DeleteProductCommandHandler(IDocumentSession session, ILogger<DeleteProductCommandHandler> logger) : ICommandHandler<DeleteProductCommand, DeleteProductResult>
{
    public async Task<DeleteProductResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("DeleteProductCommandHandler.Handle called with {@Command}", command);
        
        session.Delete<ProductEntity>(command.Id);
        await session.SaveChangesAsync(cancellationToken);

        return new DeleteProductResult(true);
    }
}