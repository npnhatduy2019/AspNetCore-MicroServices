using Contracts.Common.Interfaces;
using Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Product.API.Entities;
using Product.API.Persistence;
using Product.API.Repositories.Interfaces;

namespace Product.API.Repositories
{
    public class ProductRepository : RepositoryBaseAsync<CatalogProduct,long,ProductContext>, IProductRepository
    {
        public ProductRepository(ProductContext _context, IUnitOfWork<ProductContext> _unitofwork) : base(_context, _unitofwork)
        {

        }

        public Task<long> CreateProduct(CatalogProduct p) => CreateAsync(p);
       
        public async Task DeleteProduct(long id) 
        {
            var entity = await GetProductByID(id);
            if(entity != null) await DeleteAsync(entity);
        }


        public Task<CatalogProduct> GetProductByID(long id) =>  GetByIdAsync(id);

        public Task<CatalogProduct> GetProductByNo(string no) => FindByCondition(x => x.No.Equals(no)).SingleOrDefaultAsync();

        public async Task<IEnumerable<CatalogProduct>> GetPruducts() => await FindAll().ToListAsync();

        public Task UpdateProduct(CatalogProduct p) => UpdateAsync(p);
    }
}