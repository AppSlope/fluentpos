using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace FluentPOS.Domain.Entities
{
    public class Payment : AuditableEntity
    {
        public DateTime PaymentDate { get; set; }
        public int SaleId { get; set; }
        public virtual Sale Sale { get; set; }
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
        public int TransactionId { get; set; }
        public virtual SaleTransaction SaleTransaction { get; set; }
        public string CreditCardNumber { get; set; }
        public string CreditCardHolder { get; set; }
        public string CreditCardMonth { get; set; }
        public string CreditCardYear { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string Attachement { get; set; }
        [Column(TypeName = "text")]
        public string Note { get; set; }
        public decimal POSPaid { get; set; }
        public decimal POSBalance { get; set; }
        public string GiftCardNumber { get; set; }
        public string Reference { get; set; }
        public int StoreId { get; set; }
        public virtual Store Store { get; set; }
    }
}
