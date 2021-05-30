namespace FluentPOS.Domain.Entities
{
    public class Address : AuditableEntity
    {
        public string Country { get; set; }
        public string Province { get; set; }
        public string District { get; set; }
        public string Commune { get; set; }
        public string Village { get; set; }
        public string PostalCode { get; set; }
        public string AddressLine { get; set; }
    }
}