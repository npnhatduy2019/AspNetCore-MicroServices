using Contracts.Common.Interfaces;
using Customer.API.Entities;
using Customer.API.Persistence;
using Customer.API.Repositories.Interface;
using Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Shared.DTOs;

namespace Customer.API.Repositories
{
    public class CustomerRepository : RepositoryBaseAsync<Entities.CustomerEntity, string, CustomerContext>, ICustomerRepository
    {
        public CustomerRepository(CustomerContext _context, IUnitOfWork<CustomerContext> _unitofwork) : base(_context, _unitofwork)
        {
        }

        public async Task<string> CreateCustomer(CustomerEntity customer) {
            var result = await CreateAsync(customer);
            await SaveChangeAsync();
            return result;
        } 
        
           
            
        
        
        public async Task UpdateCustomer(CustomerEntity customer) 
        {
            await UpdateAsync(customer); 
            await SaveChangeAsync();
        }
        public async Task DeleteCustomer(string id)
        {
            var customer = await FindByCondition(c => c.Id.Equals(id)).FirstOrDefaultAsync();            
            await DeleteAsync(customer);
            await SaveChangeAsync();
        }
        
           
            
        

        public async Task<IEnumerable<CustomerEntity>> GetAll() => await FindAll().ToListAsync();

        public Task<CustomerEntity> GetCustomerByUserName(string username) => 
                                                     FindByCondition(c => c.UserName.Equals(username))
                                                                .FirstOrDefaultAsync();

        public async Task<CustomerEntity> CheckCustomer(string username, string email) =>
             await FindByCondition(c => c.UserName.Equals(username) || c.Email.Equals(email))
                    .FirstOrDefaultAsync();

        public Task<CustomerEntity> GetCustomerById(string Id)  => 
                                                     FindByCondition(c => c.Id.Equals(Id))
                                                                .FirstOrDefaultAsync();
       
    }
}