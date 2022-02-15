using Catalog.API.Entities.AuditContracts;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Catalog.API.Entities
{
    public class Product : ICreatedOn, IModifiedOn
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public string ImageFile { get; set; }
        public decimal Price { get; set; }
        public string CategoryId { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public bool IsDeleted { get; set; }
    }
}
