namespace Catalog.API.DTOs
{
    public class CategoryDTO
    {
        public string Id { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public bool IsDeleted { get; set; }
        public string ParentId { get; set; }
    }
}