using System.ComponentModel.DataAnnotations.Schema;

namespace FluentPOS.Domain.Entities
{
    public class Store : AuditableEntity
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string LogoUrl { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public Address Address { get; set; }
        public string CurrencyCode { get; set; }
        [Column(TypeName = "text")]
        public string ReceiptHeader { get; set; }
        [Column(TypeName = "text")]
        public string ReceiptFooter { get; set; }
    }
}