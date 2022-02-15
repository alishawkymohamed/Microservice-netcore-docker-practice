using System;

namespace Catalog.API.Entities.AuditContracts
{
    public interface ICreatedOn
    {
        DateTime? CreatedOn { get; set; }
    }
}
