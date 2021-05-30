namespace FluentPOS.Domain.Entities
{
    public class Supplier : AuditableEntity
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string CustomField1 { get; set; }
        public string CustomField2 { get; set; }
    }
}