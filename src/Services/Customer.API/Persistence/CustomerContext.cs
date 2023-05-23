using Contracts.Domains.Interfaces;
using Customer.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace Customer.API.Persistence
{
    public class CustomerContext : DbContext
    {
        public CustomerContext(DbContextOptions<CustomerContext> options) : base(options)
        {
            
        }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<CustomerEntity>().HasIndex(x => x.Email).IsUnique();
            builder.Entity<CustomerEntity>().HasIndex(x => x.UserName).IsUnique();
            base.OnModelCreating(builder);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var modify = ChangeTracker.Entries()
                            .Where(m => m.State == EntityState.Added 
                                        || m.State == EntityState.Modified
                                        || m.State == EntityState.Deleted);

            foreach(var item in modify)
            {
                switch(item.State)
                {
                    case EntityState.Added:
                    {
                        // if(item.Entity is IEntityBase<string> entityBase)
                        // {
                        //     entityBase.Id = Guid.NewGuid().ToString();
                        // }
                        Entry(item.Entity).Property("IsActive").CurrentValue = true;
                    }
                    break;

                    case EntityState.Modified:
                    //
                    break;
                    case EntityState.Deleted:
                    //
                    break;
                }
            }


            return base.SaveChangesAsync(cancellationToken);
        }

        public DbSet<CustomerEntity> Customers{get;set;}

    }
}