using AutoMapper;
using Catalog.API.DTOs;
using Catalog.API.Entities;
using Catalog.API.Repositories.Interfaces;
using Catalog.API.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalog.API.Services.Implementation
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository repository, IMapper mapper)
        {
            this._repository = repository;
            this._mapper = mapper;
        }

        public async Task<IEnumerable<CategoryDTO>> GetCategories()
        {
            var categories = await this._repository.GetAll();
            var converted = this._mapper.Map<IEnumerable<Category>, IEnumerable<CategoryDTO>>(categories); ;
            return converted;
        }

        public async Task<CategoryDTO> GetCategoryById(string id)
        {
            var category = await this._repository.GetById(id);
            return this._mapper.Map<Category, CategoryDTO>(category);
        }

        public async Task CreateCategory(CategoryUpsertDTO dto)
        {
            var category = this._mapper.Map<CategoryUpsertDTO, Category>(dto);
            await this._repository.Create(category);
        }

        public async Task UpdateCategory(string id, CategoryUpsertDTO dto)
        {
            var category = this._mapper.Map<CategoryUpsertDTO, Category>(dto);
            await this._repository.Update(id, category);
        }

        public async Task DeleteCategory(string id)
        {
            await this._repository.Delete(id);
        }
    }
}
