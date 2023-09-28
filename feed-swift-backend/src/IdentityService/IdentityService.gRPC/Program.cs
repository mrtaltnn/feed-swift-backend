using IdentityService.Application;
using IdentityService.Application.Model;
using IdentityService.Domain;
using IdentityService.gRPC.Services;
using IdentityService.Persistence;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Serilog;
using Shared.Helper.Exceptions;
using Shared.Helper.ServiceRegistrations;

var builder = WebApplication.CreateBuilder(args);
try
{
    builder.Host.UseSerilog();
    var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";
    var configuration = new ConfigurationBuilder()
        .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "Configuration/Settings"))
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true)
        .AddJsonFile("serilog.json", optional: false, reloadOnChange: true)
        .AddJsonFile($"serilog.{environment}.json", optional: false, reloadOnChange: true)
        .AddJsonFile($"appsettings.k8s.json", optional: true, reloadOnChange: true)
        .AddEnvironmentVariables()
        .Build();
    var appSettings = new AppSettings();
    configuration.Bind(nameof(AppSettings), appSettings);

    // Add services to the container.
    builder.Services.AddSingleton(appSettings);
    builder.Services.AddPersistence(appSettings).AddApplication(appSettings).AddDomain().AddGrpc(
        options =>
        {
            {
                options.Interceptors.Add<ExceptionGrpcInterceptor>();
                options.EnableDetailedErrors = true;
            }
        });

    if (appSettings.GrpcServiceSettings.IsUseWithoutTls)
        builder.WebHost.ConfigureKestrel(options => options.ListenAnyIP(appSettings.GrpcServiceSettings.Port, o => o.Protocols = HttpProtocols.Http2));

    const string allowAllPolicy = "AllowAll";
    builder.Services.AddGrpcReflection();
    builder.Services.AddCors(o => o.AddPolicy(name: allowAllPolicy,
        policyBuilder =>
        {
            policyBuilder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()
                .WithExposedHeaders("Grpc-Status", "Grpc-Message");
        }));
    

    var app = builder.Build();
    app.UseLogger(configuration);
    if (app.Environment.IsDevelopment())
    {
        app.MapGrpcReflectionService();
    }

    app.UseCors(allowAllPolicy);
    app.UseGrpcWeb(new GrpcWebOptions {DefaultEnabled = true});
    

// Configure the HTTP request pipeline.
    app.MapGrpcService<GreeterService>();
    app.MapGet("/",
        () =>
            "[IdentityService] Communication with gRPC endpoints must be made through a gRPC client.");

    app.Run();
}
catch (Exception e)
{
    Console.WriteLine("Error While Hosting Services: {0}", e);
    Log.Fatal(e, "An unhandled exception occurred!");
}