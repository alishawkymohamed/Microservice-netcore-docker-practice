using AutoMapper;
using Catalog.API.DTOs;
using Catalog.API.Entities;

namespace Catalog.API.Mappers
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDTO>();
            CreateMap<ProductUpsertDTO, Product>();
        }
    }
}
