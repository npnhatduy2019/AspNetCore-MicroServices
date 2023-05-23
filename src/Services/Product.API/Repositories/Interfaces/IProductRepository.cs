using Contracts.Common.Interfaces;
using Product.API.Entities;
using Product.API.Persistence;

namespace Product.API.Repositories.Interfaces
{
    public interface IProductRepository : IRepositoryBaseAsync<CatalogProduct,long,ProductContext>
    {
        Task<IEnumerable<CatalogProduct>> GetPruducts();
        Task<CatalogProduct> GetProductByID(long id);

        Task<CatalogProduct> GetProductByNo(string no);

        Task<long> CreateProduct(CatalogProduct p);

        Task UpdateProduct(CatalogProduct p);

        Task DeleteProduct(long id);

        
    }
}