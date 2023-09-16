using Shared.Helper.Services.GrpcServiceClient;

namespace IdentityService.Application.Model;

public sealed class AppSettings
{
    public bool IsSwaggerActive { get; set; }
    public AppGrpcServiceSettings GrpcServiceSettings { get; set; } = null!;
}

public sealed class AppGrpcServiceSettings : GrpcServiceSettings
{
    public bool IsUseWithoutTls { get; set; }
}