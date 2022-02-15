using System;

namespace Catalog.API.DTOs
{
    public class ProductDTO
    {
        public string Id { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public string ImageFile { get; set; }
        public decimal Price { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string CategoryId { get; set; }
        public bool IsDeleted { get; set; }
    }
}
