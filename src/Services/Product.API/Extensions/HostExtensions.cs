using Microsoft.EntityFrameworkCore;

namespace Product.API.Extensions
{
    public static class HostExtensions
    {
        public static IHost MigrationsDatabase<Tcontext>(this IHost host, Action<Tcontext, IServiceProvider> seeder)
        where Tcontext: DbContext
        {
            using(var Scope = host.Services.CreateScope())
            {
                var services = Scope.ServiceProvider;
                var Logger = services.GetRequiredService<ILogger<Tcontext>>();
                var context = services.GetRequiredService<Tcontext>();

                try
                {
                    Logger.LogInformation("Migrating Database Context");
                    ExecuteMigrations(context);
                    Logger.LogInformation("Migrated Database Context");
                    InvokeSeeder(seeder, context, services);
                    Logger.LogInformation($"Seed data for {nameof(context)}");

                }catch(Exception ex)
                {
                    Logger.LogInformation("An error occurred while migrating the mysql database");
                }
            }

            return host;
        }

        private static void ExecuteMigrations<Tcontext>(Tcontext context)
        where Tcontext: DbContext
        {
            context.Database.Migrate();
        }

        private static void InvokeSeeder<Tcontext>(Action<Tcontext, IServiceProvider> seeder, Tcontext context, IServiceProvider services)
        where Tcontext: DbContext
        {
            seeder(context, services);
        }
    }
}