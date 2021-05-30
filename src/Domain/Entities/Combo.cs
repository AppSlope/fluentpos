using System.Collections.Generic;

namespace FluentPOS.Domain.Entities
{
    public class Combo : AuditableEntity
    {
        public string Code { get; set; }
        public IEnumerable<ComboItem> ComboItems { get; set; }
    }
    public class ComboItem : AuditableEntity
    {
        public string Code { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Cost { get; set; }
    }
}

