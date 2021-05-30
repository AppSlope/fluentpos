using System;

namespace FluentPOS.Domain.Entities
{
    public class GiftCard : AuditableEntity
    {
        public string Code { get; set; }
        public DateTime EffectiveDate { get; set; }
        public decimal Value { get; set; }
        public decimal Balance { get; set; }
        public int CustomerId { get; set; }
        public virtual int Customer { get; set; }
        public DateTime ExpiryDate { get; set; }
        public int StoreId { get; set; }
        public virtual int Store { get; set; }
    }
}
