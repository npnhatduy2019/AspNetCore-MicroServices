
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Product.API.Persistence;

namespace Product.API.Extensions
{
    public static class ServiceExtensions
    {
       public static IServiceCollection AddInfrastructure(this IServiceCollection services,IConfiguration configuration)
       {
            services.AddControllers();
            services.Configure<RouteOptions>(optinons => optinons.LowercaseUrls=true);
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            //mysql 
            services.ConfigureProductDbContext(configuration);
            
            return services;
       }

       public static IServiceCollection ConfigureProductDbContext(this IServiceCollection services, IConfiguration configuration)
       {
            var connectionString = configuration.GetConnectionString("defaultConnectionString");
            var builder = new MySqlConnectionStringBuilder(connectionString);
            services.AddDbContext<ProductContext>(options => options.UseMySql(builder.ConnectionString, 
                    ServerVersion.AutoDetect(builder.ConnectionString), e => {

                        e.MigrationsAssembly("Product.API");
                        e.SchemaBehavior(MySqlSchemaBehavior.Ignore);
                    }));

            // services.AddDbContext<ProductContext>(options =>
            //         options.UseMySQL(builder.ConnectionString,
            //             new MySQLVersion(new Version(8, 0, 26)), // thay vì sử dụng ServerVersion.AutoDetect
            //             mysqlOptions => mysqlOptions
            //                 .CharSetBehavior(CharSetBehavior.NeverAppend)
            //                 .EnableRetryOnFailure())
            //     );
            return services;
       }
    }
}