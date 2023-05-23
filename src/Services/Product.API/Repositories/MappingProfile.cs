using AutoMapper;
using Infrastructure.Mappings;
using Product.API.Entities;
using Shared.DTOs;

namespace Product.API.Repositories
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CatalogProduct,ProductDTO>();
            CreateMap<CreateProductDTO,CatalogProduct>();
            CreateMap<CatalogProduct, UpdateProductDTO>();
            CreateMap<UpdateProductDTO, CatalogProduct>();
                //.IgnoreAllNonExisting();
        }
    }
}