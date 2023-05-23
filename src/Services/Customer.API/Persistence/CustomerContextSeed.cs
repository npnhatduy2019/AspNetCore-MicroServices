using Customer.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace Customer.API.Persistence
{
    public static class CustomerContextSeed 
    {
        public static async Task<IHost> SeedCustomerDataAsync(this IHost host)
        {
            using var scope = host.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<CustomerContext>();

            await context.Database.MigrateAsync();

            await SeedCustomerAsync(context, "admin", "admin", "system", "admin@microservices.com", "123 LVK, NY", "+84966851444");
            await SeedCustomerAsync(context, "user", "user", "user", "user@microservices.com", "456 NTD, NY", "+84966851445");

            return host;
        }

        public static async Task SeedCustomerAsync(CustomerContext context, string user, string Lname, string Fname, string email, string addr, string phone)
        {
            var customerExists = await context.Customers.AnyAsync(c => c.UserName.Equals(user) || c.UserName.Equals(email));
            if (!customerExists)
            {
                var cust = new CustomerEntity
                {
                    Id = Guid.NewGuid().ToString(),
                    UserName = user,
                    LName = Lname,
                    FName = Fname,
                    Email = email,
                    Address = addr,
                    Phone = phone
                };

                context.Customers.Add(cust);
                await context.SaveChangesAsync();
            }
        }


    }
}