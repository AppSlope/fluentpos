namespace FluentPOS.Domain.Entities
{
    public class Printer : AuditableEntity
    {
        public string Title { get; set; }
        public string Type { get; set; }
        public string Profile { get; set; }
        public int  CharPerLine { get; set; }
        public string Path { get; set; }
        public string IpAddress { get; set; }
        public string Port { get; set; }
    }
}