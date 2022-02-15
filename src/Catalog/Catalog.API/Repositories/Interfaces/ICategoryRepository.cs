using Catalog.API.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalog.API.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAll();
        Task<IEnumerable<Category>> GetChildren(string id);
        Task<Category> GetById(string id);
        Task Create(Category category);
        Task Update(string id, Category category);
        Task Delete(string id);
    }
}
