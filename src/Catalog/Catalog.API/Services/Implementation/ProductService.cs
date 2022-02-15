using AutoMapper;
using Catalog.API.DTOs;
using Catalog.API.Entities;
using Catalog.API.Repositories.Interfaces;
using Catalog.API.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalog.API.Services.Implementation
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository repository, IMapper mapper)
        {
            this._repository = repository;
            this._mapper = mapper;
        }

        public async Task<IEnumerable<ProductDTO>> GetProducts()
        {
            var products = await this._repository.GetAll();
            return this._mapper.Map<IEnumerable<ProductDTO>>(products);
        }

        public async Task<ProductDTO> GetProductById(string id)
        {
            var prod = await this._repository.GetById(id);
            return this._mapper.Map<ProductDTO>(prod);
        }

        public async Task<ProductDTO> CreateProduct(ProductUpsertDTO dto)
        {
            var prod = this._mapper.Map<Product>(dto);
            var created = await this._repository.Create(prod);
            return this._mapper.Map<ProductDTO>(created);
        }

        public async Task<ProductDTO> UpdateProduct(string id, ProductUpsertDTO dto)
        {
            var prod = await this._repository.GetById(id);
            var updated = await this._repository.Update(id, prod);
            return this._mapper.Map<ProductDTO>(updated);
        }

        public async Task DeleteProduct(string id)
        {
            await this._repository.Delete(id);
        }

        public async Task<ProductDTO> GetProductByName(string name)
        {
            var prod = await this._repository.GetByName(name);
            return this._mapper.Map<ProductDTO>(prod);
        }

        public async Task<IEnumerable<ProductDTO>> GetProductsByCategory(string categoryId)
        {
            var prod = await this._repository.GetByCategoryId(categoryId);
            return this._mapper.Map<IEnumerable<ProductDTO>>(prod);
        }
    }
}
