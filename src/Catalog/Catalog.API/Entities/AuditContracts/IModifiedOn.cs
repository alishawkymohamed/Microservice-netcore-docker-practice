using System;

namespace Catalog.API.Entities.AuditContracts
{
    public interface IModifiedOn
    {
        DateTime? ModifiedOn { get; set; }
    }
}
