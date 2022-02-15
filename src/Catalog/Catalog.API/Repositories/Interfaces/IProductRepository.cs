using Catalog.API.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalog.API.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAll();
        Task<Product> GetById(string id);
        Task<Product> GetByName(string name);
        Task<IEnumerable<Product>> GetByCategoryId(string categoryId);
        Task<Product> Create(Product product);
        Task<Product> Update(string id, Product product);
        Task Delete(string id);
    }
}
