using MediatR;
using Microsoft.Extensions.Logging;
using SharedKernel;

namespace Application.Abstractions.Behaviors;

internal sealed class RequestLoggingPipelineBehavior<TRequest, TResponse>(
    ILogger<RequestLoggingPipelineBehavior<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("Handling {RequestName}", typeof(TRequest).Name);

        var response = await next();

        logger.LogInformation("Handled {RequestName}", typeof(TRequest).Name);

        return response;
    }
}
