using Catalog.API.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalog.API.Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDTO>> GetProducts();
        Task<ProductDTO> GetProductById(string id);
        Task<ProductDTO> GetProductByName(string name);
        Task<IEnumerable<ProductDTO>> GetProductsByCategory(string categoryId);
        Task<ProductDTO> CreateProduct(ProductUpsertDTO dto);
        Task<ProductDTO> UpdateProduct(string id, ProductUpsertDTO dto);
        Task DeleteProduct(string id);
    }
}
