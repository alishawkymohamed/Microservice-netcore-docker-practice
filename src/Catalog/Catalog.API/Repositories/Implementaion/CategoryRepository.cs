using Catalog.API.Data.Interfaces;
using Catalog.API.Entities;
using Catalog.API.Helpers;
using Catalog.API.Repositories.Interfaces;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Catalog.API.Repositories.Implementaion
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ICatalogContext _context;

        public CategoryRepository(ICatalogContext catalogContext)
        {
            this._context = catalogContext;
        }

        public async Task<IEnumerable<Category>> GetAll()
        {
            var categories = await this._context.Categories.FindAsync(cat => cat.IsDeleted == false);
            return await categories.ToListAsync();
        }

        public async Task<IEnumerable<Category>> GetChildren(string id)
        {
            var categories = await this._context.Categories.FindAsync(cat => cat.IsDeleted == false && string.Equals(cat.ParentId, id));
            return await categories.ToListAsync();
        }

        public async Task<Category> GetById(string id)
        {
            var categories = await this._context.Categories.FindAsync(cat => cat.IsDeleted == false && string.Equals(cat.Id, id));

            return await categories.FirstOrDefaultAsync();
        }

        public async Task Create(Category category)
        {
            if (category.ParentId != null)
            {
                var parent = await this.GetById(category.ParentId);
                if (parent == null)
                    throw new BusinessException(((int)HttpStatusCode.BadRequest).ToString(), "ParentId is not valid value !");
            }
            category.CreatedOn = DateTime.Now;
            await this._context.Categories.InsertOneAsync(category);
        }

        public async Task Update(string id, Category category)
        {
            var cat = await this.GetById(id);
            if (cat != null)
            {
                if (category.ParentId != null)
                {
                    var parent = await this.GetById(category.ParentId);
                    if (parent == null)
                        throw new BusinessException(((int)HttpStatusCode.BadRequest).ToString(), "ParentId is not valid value !");
                }
                category.Id = id;
                category.ModifiedOn = DateTime.Now;
                await this._context.Categories.ReplaceOneAsync(x => string.Equals(x.Id, id), category);
            }
        }

        public async Task Delete(string id)
        {
            var category = await this.GetById(id);
            if (category != null)
            {
                var hasChildren = this._context.Categories.CountDocuments(cat => string.Equals(cat.ParentId, id)) > 0;

                if (hasChildren)
                    throw new BusinessException(((int)HttpStatusCode.Forbidden).ToString(), "Can't delete category before deleting its children");

                category.IsDeleted = true;

                await this.Update(id, category);
            }
        }
    }
}