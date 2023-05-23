using Shared.DTOs;

namespace Customer.API.Services.Interface
{
    public interface ICustomerServices
    {
        Task<IResult> GetCustomerByUsername(string username);
        Task<IResult> GetCustomer();

        Task<IResult> CreateCustomer(CreateCustomerDTO customerDTO);

        Task<IResult> UpdateCustomer(string id, UpdateCustomerDTO customerDTO);

        Task<IResult> DeleteCustomer(string id);

        Task<IResult> GetCustomerById(string id);

    }
}