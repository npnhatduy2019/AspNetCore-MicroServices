using Contracts.Common.Interfaces;
using Customer.API.Entities;
using Customer.API.Persistence;
using Shared.DTOs;

namespace Customer.API.Repositories.Interface
{
    public interface ICustomerRepository:IRepositoryQueryBase<Entities.CustomerEntity, string, CustomerContext>
    {
        Task<CustomerEntity> GetCustomerByUserName(string username);

        Task<CustomerEntity> GetCustomerById(string Id);

        Task<CustomerEntity> CheckCustomer(string username, string email);

        Task<IEnumerable<Entities.CustomerEntity>> GetAll();

        Task<string> CreateCustomer(CustomerEntity customer);

        Task UpdateCustomer(CustomerEntity customer);

        Task DeleteCustomer(string id);
        
    }
}