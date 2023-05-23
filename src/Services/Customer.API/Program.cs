using Common.Logging;
using Contracts.Common.Interfaces;
using Customer.API.Persistence;
using Customer.API.Repositories;
using Customer.API.Repositories.Interface;
using Customer.API.Services;
using Customer.API.Services.Interface;
using Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Shared.DTOs;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog(SeriLogger.Configure);

Log.Information("Start Customer API up");
// Add services to the container. 
try
{
    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    var connectstring = builder.Configuration.GetConnectionString("DefaultConnectionString");
    builder.Services.AddDbContext<CustomerContext>(option => {
        option.UseNpgsql(connectstring);
    });

    builder.Services.AddScoped<ICustomerRepository,CustomerRepository>()
                        .AddScoped(typeof(IRepositoryBaseAsync<,,>), typeof(RepositoryBaseAsync<,,>))
                        .AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWok<>))
                        .AddScoped<ICustomerServices,CustomerServices>();

    builder.Services.AddAutoMapper(cf => cf.AddProfile(new MapperProfile()));                        
    
    var app = builder.Build();

    
    app.MapGet("/", () => "Hello Minimal API Customer !");

    app.MapGet("/api/Customer", async (ICustomerServices service) => await service.GetCustomer());

    app.MapGet("/api/Customer/{username}", async (string username, ICustomerServices service) => await service.GetCustomerByUsername(username));
    app.MapGet("api/customer/CustomerById/{id}", (string id, ICustomerServices services) => services.GetCustomerById(id));

    app.MapPost("api/customer",(CreateCustomerDTO customer, ICustomerServices services) => 
            services.CreateCustomer(customer)
    );

    app.MapPut("/api/customer/{id}", (string id, UpdateCustomerDTO customer, ICustomerServices services) => services.UpdateCustomer(id, customer));

    app.MapDelete("/api/Customer/{id}", (string id, ICustomerServices services) => services.DeleteCustomer(id));

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    // app.SeedCustomerData()
    //     .Run();
    app.SeedCustomerDataAsync().Wait();
    app.Run();


}catch(Exception ex)
{
    string type = ex.GetType().Name;
    if(type.Equals("StopTheHostException",StringComparison.Ordinal))
    {
        throw;
    }
    Log.Fatal(ex, "Unhandled Exception");}
finally
{
    Log.Information("Shutdown Customer API Complete");
    Log.CloseAndFlush();
}
