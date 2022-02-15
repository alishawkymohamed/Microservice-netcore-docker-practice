namespace Catalog.API.Settings
{
    public interface ICatalogDatabaseSettings
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
        string ProductCollectionName { get; set; }
        string CategoryCollectionNmae { get; set; }
    }
}
