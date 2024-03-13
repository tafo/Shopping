using MediatR;

namespace Common.CQRS;

public interface ICommand<out TResponse> : IRequest<TResponse>
{
    
}

public interface ICommand : ICommand<Unit>
{
    
}