using System.Collections.Generic;

namespace FluentPOS.Domain.Entities
{
    public abstract class Person : AuditableEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public List<Address> Addresses { get; set; }
    }
}