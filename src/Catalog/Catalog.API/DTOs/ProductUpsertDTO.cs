namespace Catalog.API.DTOs
{
    public class ProductUpsertDTO
    {
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public string ImageFile { get; set; }
        public decimal Price { get; set; }
        public string CategoryId { get; set; }
    }
}
