using AutoMapper;
using Customer.API.Entities;
using Shared.DTOs;

namespace Customer.API.Repositories
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<UpdateCustomerDTO, CustomerEntity>();
        }

    }
}