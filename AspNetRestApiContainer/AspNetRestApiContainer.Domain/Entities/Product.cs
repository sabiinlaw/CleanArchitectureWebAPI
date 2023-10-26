using AspNetRestApiContainer.Domain.Common;

namespace AspNetRestApiContainer.Domain.Entities
{
    public class Product : AuditableBaseEntity
    {
        public string Number { get; set; }
        public decimal Price { get; set; }
    }
}