using Grpc.Net.Client;
using Grpc.Net.Client.Web;
using Microsoft.Extensions.Logging;

namespace Shared.Helper.Services.GrpcServiceClient;

public interface IGrpcServiceClientFactory
{
    public T? CreateServiceClient<T>(GrpcServiceSettings settings) where T : Grpc.Core.ClientBase;
}

public class GrpcServiceClientFactory : IGrpcServiceClientFactory
{
    private readonly ILoggerFactory _loggerFactory;

    public GrpcServiceClientFactory(ILoggerFactory loggerFactory)
    {
        _loggerFactory = loggerFactory;
    }

    public T? CreateServiceClient<T>(GrpcServiceSettings settings) where T : Grpc.Core.ClientBase
    {
        var channelOptions = GrpcChannelOptions();
        var channel = GrpcChannel.ForAddress($"{settings.Protocol}://{settings.Host}:{settings.Port}", channelOptions);
        return Activator.CreateInstance(typeof(T), channel) as T;
    }

    private GrpcChannelOptions GrpcChannelOptions()
    {
        var handler = new GrpcWebHandler(GrpcWebMode.GrpcWebText, new HttpClientHandler());
        var httpClient = new HttpClient(handler);
        var channelOptions = new GrpcChannelOptions
        {
            HttpClient = httpClient,
            LoggerFactory = _loggerFactory,
            MaxReceiveMessageSize = null,
            MaxSendMessageSize = null
             
        };
        return channelOptions;
    }
}