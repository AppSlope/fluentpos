using System;

namespace FluentPOS.Domain.Entities
{
    public class SaleTransaction : AuditableEntity
    {
        public string PaymentType { get; set; }
        public DateTime TransactionTime { get; set; }
        public decimal Amount { get; set; }
        public decimal TenderedAmount { get; set; }
        public int InvoiceId { get; set; }
        public virtual Invoice Invoice { get; set; }
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
        public int UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
        public int StoreId { get; set; }
        public virtual Store Store { get; set; }

    }
}