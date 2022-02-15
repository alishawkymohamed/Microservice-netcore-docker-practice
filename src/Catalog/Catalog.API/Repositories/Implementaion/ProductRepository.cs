using AutoMapper;
using Catalog.API.Data.Interfaces;
using Catalog.API.Entities;
using Catalog.API.Repositories.Interfaces;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalog.API.Repositories.Implementaion
{
    public class ProductRepository : IProductRepository
    {
        private readonly ICatalogContext _context;
        private readonly IMapper _mapper;

        public ProductRepository(ICatalogContext context, IMapper mapper)
        {
            this._context = context ?? throw new ArgumentException(nameof(context));
            this._mapper = mapper ?? throw new ArgumentException(nameof(mapper));
        }

        public async Task<Product> Create(Product product)
        {
            product.CreatedOn = DateTime.Now;
            await _context.Products.InsertOneAsync(product);
            return product;
        }

        public async Task<Product> GetById(string id)
        {
            return await this._context.Products.Find(p => p.Id == id && p.IsDeleted == false).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Product>> GetByCategoryId(string categoryId)
        {
            return await this._context.Products.Find(p => p.CategoryId == categoryId && p.IsDeleted == false).ToListAsync();
        }

        public async Task<Product> GetByName(string name)
        {
            return await this._context.Products
                .Find(p => p.IsDeleted == false && (p.NameAr.ToLower() == name.ToLower() || p.NameEn.ToLower() == name.ToLower()))
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            return await this._context.Products.Find(p => p.IsDeleted == false).ToListAsync();
        }

        public async Task<Product> Update(string id, Product product)
        {
            var prod = await this.GetById(id);
            if (prod != null)
            {
                product.Id = id;
                product.ModifiedOn = DateTime.Now;
                await this._context.Products.ReplaceOneAsync(x => string.Equals(x.Id, id), product);
            }
            return await this.GetById(id);
        }

        public async Task Delete(string id)
        {
            var prod = await this.GetById(id);
            if (prod != null)
            {
                prod.IsDeleted = true;
                await this.Update(id, prod);
            }
        }
    }
}
