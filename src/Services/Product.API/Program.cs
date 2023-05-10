using Common.Logging;
using Serilog;
using Product.API.Extensions;
using Product.API.Persistence;

var builder = WebApplication.CreateBuilder(args);

Log.Information("Start Product API up");
// Add services to the container. 
try
{
    builder.Host.UseSerilog(SeriLogger.Configure);
    //builder.Host.AddAppConfigurations();
    builder.AddAppConfigurations();
    builder.Services.AddInfrastructure(builder.Configuration);
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    
    var app = builder.Build();
    app.UseInfrastructure();  
    app.MigrationsDatabase<ProductContext>((context, _) => {
        SeedProductContext.SeedProduct(context,Log.Logger).Wait();
        })
        .Run();
    //app.Run();


}catch(Exception ex)
{
    // string type = ex.GetType().Name;
    // if(type.Equals("StopTheHostException",StringComparison.Ordinal))
    // {
    //     throw;
    // }
    Log.Fatal(ex, "Unhandled Exception");
}
finally
{
    Log.Information("Shutdown Product API Complete");
    Log.CloseAndFlush();
}
