using System.ComponentModel.DataAnnotations.Schema;

namespace FluentPOS.Domain.Entities
{
    public class Product : AuditableEntity
    {
        public string Name { get; set; }
        public string LocaleName { get; set; }
        public int CategoryId { get; set; }
        public virtual ProductCategory Category { get; set; }
        public int BrandId { get; set; }
        public virtual ProductBrand Brand { get; set; }
        public decimal Price { get; set; }
        public decimal Cost { get; set; }
        public string ImageUrl { get; set; }
        public string Tax { get; set; }
        public string TaxMethod { get; set; }
        public string BarcodeSymbology { get; set; }
        public decimal AlertQuantity { get; set; }
        [Column(TypeName = "text")]
        public string Detail { get; set; }
    }
}