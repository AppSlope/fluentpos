namespace FluentPOS.Domain.Entities
{
    internal class BusinessUnit
    {
        public Company Details { get; set; }
        public BusinessType BusinessType { get; set; }
        public string NormalizedBusinessType { get; set; }
    }

    public enum BusinessType
    {
        Restaurant,
        Retail,
        Warehouse,
        Bakery
    }
}