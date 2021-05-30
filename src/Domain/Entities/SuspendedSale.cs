using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace FluentPOS.Domain.Entities
{
    public class SuspendedSale : AuditableEntity
    {
        public DateTime Date { get; set; }
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
        public string CustomerName { get; set; }
        public decimal ProductDiscount { get; set; }
        public decimal OrderDiscount { get; set; }
        public decimal TotalDiscount { get; set; }
        public decimal ProductTax { get; set; }
        public decimal Total { get; set; }
        public decimal GrandTotal { get; set; }
        public int TotalItems { get; set; }
        public decimal TotalQuantity { get; set; }
        public decimal Paid { get; set; }
        [Column(TypeName = "text")]
        public string Note { get; set; }
        public string Status { get; set; }
        public decimal Rounding { get; set; }
        public string HoldReference { get; set; }
        public int StoreId { get; set; }
        public virtual Store Store { get; set; }
        public IEnumerable<SuspendedSaleDetail> SuspendedSaleDetails { get; set; }
    }
}