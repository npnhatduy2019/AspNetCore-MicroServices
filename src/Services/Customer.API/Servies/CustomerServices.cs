using AutoMapper;
using Customer.API.Entities;
using Customer.API.Repositories.Interface;
using Customer.API.Services.Interface;
using Shared.DTOs;

namespace Customer.API.Services
{
    public class CustomerServices : ICustomerServices
    {
        private readonly ICustomerRepository repo;
        private readonly IMapper mapper;

        public CustomerServices(ICustomerRepository _repo, IMapper _mapper)
        {
            repo = _repo;
            mapper = _mapper;
        }

        public async Task<IResult> CreateCustomer(CreateCustomerDTO customerDTO)
        {
            var exists = await repo.CheckCustomer(customerDTO.UserName, customerDTO.Email);
            if(exists !=null) return Results.BadRequest();
            var result = await repo.CreateCustomer(new CustomerEntity{
                Id = Guid.NewGuid().ToString(),
                UserName = customerDTO.UserName,
                FName = customerDTO.FName,
                LName = customerDTO.LName,
                Email = customerDTO.Email,
                Address = customerDTO.Address,
                Phone = customerDTO.Phone                
            });
            return Results.Ok(result);
        }

        public async Task<IResult> DeleteCustomer(string id)
        {
            var exists = await repo.GetCustomerById(id);
            if(exists == null) return Results.NotFound();
            await repo.DeleteCustomer(id);
            return Results.Ok();

        }

        public async Task<IResult> GetCustomer() => Results.Ok(await repo.GetAll());

        public async Task<IResult> GetCustomerById(string id) => Results.Ok(await repo.GetCustomerById(id));

        public async Task<IResult> GetCustomerByUsername(string username) => Results.Ok(await repo.GetCustomerByUserName(username));

        public async Task<IResult> UpdateCustomer(string id, UpdateCustomerDTO customerDTO)
        {
            var exists = await repo.GetCustomerById(id);
            if(exists == null) return Results.NotFound();

            var customer = mapper.Map<UpdateCustomerDTO,CustomerEntity>(customerDTO, exists);
            await repo.UpdateCustomer(customer);
            
            return Results.Ok(customer);
        }
    }
}