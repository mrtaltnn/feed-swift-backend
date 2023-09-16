using IdentityService.Application;
using IdentityService.Application.Model;
using IdentityService.Domain;
using IdentityService.Persistence;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

try
{
    var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";
    var configuration = new ConfigurationBuilder()
        .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "Configuration/Settings"))
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true)
        .AddJsonFile($"appsettings.k8s.json", optional: true, reloadOnChange: true)
        .AddEnvironmentVariables()
        .Build();
    var appSettings = new AppSettings();
    configuration.Bind(nameof(AppSettings), appSettings);
    
// Add services to the container.
    builder.Services.AddPersistence(appSettings).AddApplication(appSettings).AddDomain(appSettings).AddControllers();

 // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
 builder.Services.AddEndpointsApiExplorer();
 builder.Services.AddSwaggerGen();
 
 var app = builder.Build();
 
 // Configure the HTTP request pipeline.
 if (appSettings.IsSwaggerActive)
 {
     app.UseSwagger();
     app.UseSwaggerUI();
 }
 
 app.UseHttpsRedirection();
 
 app.UseAuthorization();
 
 app.MapControllers();
 
 app.Run();
}
catch (Exception e)
{
    Console.WriteLine("Error While Hosting Services: {0}",e);
    Log.Fatal(e, "An unhandled exception occurred!");
}
