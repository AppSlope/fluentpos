namespace FluentPOS.Domain.Entities
{
    internal class SaleDetail : AuditableEntity
    {
        public Product Product { get; set; }
    }
}