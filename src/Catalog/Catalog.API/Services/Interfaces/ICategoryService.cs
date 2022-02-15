using Catalog.API.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalog.API.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDTO>> GetCategories();
        Task<CategoryDTO> GetCategoryById(string id);
        Task CreateCategory(CategoryUpsertDTO dto);
        Task UpdateCategory(string id, CategoryUpsertDTO dto);
        Task DeleteCategory(string id);
    }
}
