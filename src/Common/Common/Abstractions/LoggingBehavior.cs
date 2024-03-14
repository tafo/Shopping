using System.Diagnostics;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Common.Abstractions;

public class LoggingBehavior<TRequest, TResponse>(ILogger<LoggingBehavior<TRequest, TResponse>> logger) : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse> where TResponse : notnull
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        logger.LogInformation("[START] Handle request={RequestName} - Response={ResponseName} - RequestData={RequestData}", typeof(TRequest).Name, typeof(TResponse).Name, request);

        var timer = new Stopwatch();
        timer.Start();

        var response = await next();

        timer.Stop();
        var timeTaken = timer.Elapsed;
        if (timeTaken.Seconds > 3)
        {
            logger.LogWarning("[PERFORMANCE] The request {RequestName} took {TimeTaken} seconds.", typeof(TRequest).Name, timeTaken.Seconds);
        }
        
        logger.LogInformation("[END] Handled {RequestName} with {ResponseName}", typeof(TRequest).Name, typeof(TResponse).Name);
        
        return response;
    }
}