using Catalog.API.Data.Interfaces;
using Catalog.API.Entities;
using Catalog.API.Settings;
using MongoDB.Driver;

namespace Catalog.API.Data.Implementaion
{
    public class CatalogContext : ICatalogContext
    {
        private readonly ICatalogDatabaseSettings settings;
        private MongoClient client;
        internal IMongoDatabase database;

        public CatalogContext(ICatalogDatabaseSettings settings)
        {
            this.settings = settings;
            InitDB();

            CatalogSeeder.Seed(Categories).Wait();
        }

        public IMongoCollection<Product> Products
        {
            get
            {
                return this.database.GetCollection<Product>(settings.ProductCollectionName);
            }
        }
        public IMongoCollection<Category> Categories
        {
            get
            {
                return this.database.GetCollection<Category>(settings.CategoryCollectionNmae);
            }
        }

        private void InitDB()
        {
            client = new MongoClient(settings.ConnectionString);
            database = client.GetDatabase(settings.DatabaseName);
        }
    }
}
