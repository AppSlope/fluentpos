namespace FluentPOS.Domain.Entities
{
    public class Product : AuditableEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Unit { get; set; }
        public string LocaleName { get; set; }
        public string Barcode { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
    }
}