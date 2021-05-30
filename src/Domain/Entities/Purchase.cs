using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace FluentPOS.Domain.Entities
{
    public class Purchase : AuditableEntity
    {
        public DateTime Date { get; set; }
        public decimal Total { get; set; }
        public string Attachement { get; set; }
        public int SupplierId { get; set; }
        public virtual Supplier Supplier { get; set; }
        public bool Received { get; set; }
        public string Reference { get; set; }
        [Column(TypeName = "text")]
        public string Note { get; set; }
        public int StoreId { get; set; }
        public virtual Store Store { get; set; }
    }

    public class PurchaseItem : AuditableEntity
    {
        public int PurchaseId { get; set; }
        public virtual Purchase Purchase { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        public decimal Quantity { get; set; }
        public decimal Cost { get; set; }
        public decimal SubTotal { get; set; }
    }
}