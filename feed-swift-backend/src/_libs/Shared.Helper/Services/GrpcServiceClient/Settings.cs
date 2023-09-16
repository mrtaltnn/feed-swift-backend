namespace Shared.Helper.Services.GrpcServiceClient;

public abstract class GrpcServiceSettings
{
    public string Protocol { get; set; } = "http";
    public required string Host { get; set; }
    public int Port { get; set; }
    public string? CertFileName { get; set; }
    public string? CertPassword { get; set; }
}