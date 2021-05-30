using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace FluentPOS.Domain.Entities
{
    public class Register : AuditableEntity
    {
        public DateTime RegisterOn { get; set; }
        public int RegisterOnUserId { get; set; }
        public virtual ApplicationUser RegisterOnUser { get; set; }
        public decimal CashInHand { get; set; }
        public string Status { get; set; }
        public decimal TotalCash { get; set; }
        public int TotalCheques { get; set; }
        public int TotalCreditCardSlips { get; set; }
        public decimal TotalCashSubmitted { get; set; }
        public int TotalChequesSubmitted { get; set; }
        public int TotalCreditCardSlipsSubmitted { get; set; }
        [Column(TypeName = "text")]
        public string Note { get; set; }
        public string TransferOpenedBills { get; set; }
        public DateTime CloseOn { get; set; }
        public int CloseByUserId { get; set; }
        public virtual ApplicationUser CloseByUser { get; set; }
        public int StoreId { get; set; }
        public virtual Store Store { get; set; }
    }
}
