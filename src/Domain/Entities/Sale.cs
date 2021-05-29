using System.Collections.Generic;

namespace FluentPOS.Domain.Entities
{
    internal class Sale
    {
        public decimal Total { get; set; }
        public decimal Tax { get; set; }
        public decimal SubTotal { get; set; }
        public IEnumerable<SaleDetail> SaleDetails { get; set; }
    }
}