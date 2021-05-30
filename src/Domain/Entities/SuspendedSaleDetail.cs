namespace FluentPOS.Domain.Entities
{
    public class SuspendedSaleDetail : AuditableEntity
    {
        public int SuspendedSaleId { get; set; }
        public virtual SuspendedSale SuspendedSale { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal NetUnitPrice { get; set; }
        public string Discount { get; set; }
        public decimal DiscountItem { get; set; }
        public int Tax { get; set; }
        public decimal TaxItem { get; set; }
        public decimal SubTotal { get; set; }
        public decimal RealUnitPrice { get; set; }
        public decimal Cost { get; set; }
        public string Comment { get; set; }
    }
}
