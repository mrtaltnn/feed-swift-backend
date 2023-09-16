using Grpc.Core;
using Grpc.Core.Interceptors;
using Microsoft.Extensions.Logging;

namespace Shared.Helper.Exceptions;

public sealed class ExceptionGrpcInterceptor : Interceptor
{
    private readonly ILogger<ExceptionGrpcInterceptor> _logger;

    public ExceptionGrpcInterceptor(ILogger<ExceptionGrpcInterceptor> logger)
    {
        _logger = logger;
    }

    public override async Task<TResponse> UnaryServerHandler<TRequest, TResponse>(
        TRequest request,
        ServerCallContext context,
        UnaryServerMethod<TRequest, TResponse> continuation)
    {
        try
        {
            return await continuation(request, context);
        }
        catch (AppException e)
        {
            _logger.LogInformation(e, "{Type} {Status} {CreatedDate} - {@Data}", "AppException", "Info", DateTime.UtcNow,e.Data);
            throw;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "{Type} {Status} {CreatedDate}", "Exception", "Error", DateTime.UtcNow);
            throw;
        }
    }
}