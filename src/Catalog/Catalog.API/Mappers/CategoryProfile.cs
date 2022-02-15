using AutoMapper;
using Catalog.API.DTOs;
using Catalog.API.Entities;

namespace Catalog.API.Mappers
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryDTO>();
            CreateMap<CategoryUpsertDTO, Category>();
        }
    }
}
