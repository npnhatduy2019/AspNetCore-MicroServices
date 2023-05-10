using System.Collections.Generic;
using Product.API.Entities;
using ILogger = Serilog.ILogger;

namespace Product.API.Persistence
{
    public static class SeedProductContext
    {
        public static async Task SeedProduct(ProductContext context, ILogger log)
        {
            if(!context.Products.Any())
            {
                //seed
                await context.Products.AddRangeAsync(GetCatalogProducts());
                await context.SaveChangesAsync();
                log.Information($"Seed Data Products Complete for {nameof(context)} !");
            }
        }

        private static IEnumerable<CatalogProduct> GetCatalogProducts()
        {
            return new List<CatalogProduct>{
                new CatalogProduct{
                    No = "3000111",
                    Name = "Product Gen 1",
                    Summary = "Product Summary",
                    Description = "Sản phẩm gen 1",
                    Price = 35500
                },
                 new CatalogProduct{
                    No = "3000112",
                    Name = "Product Gen 2",
                    Summary = "Product Summary 2",
                    Description = "Sản phẩm gen 2",
                    Price = 50500
                }
            };
        }
    }
    
}