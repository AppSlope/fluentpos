using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace FluentPOS.Domain.Entities
{
    public class Expense : AuditableEntity
    {
        public DateTime Date { get; set; }
        public string Reference { get; set; }
        public decimal Amount { get; set; }
        public string Attachement { get; set; }
        [Column(TypeName = "text")]
        public string Note { get; set; }
        public int StoreId { get; set; }
        public Store Store { get; set; }
    }
}
