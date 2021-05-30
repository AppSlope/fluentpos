namespace FluentPOS.Shared.ViewModels.Products
{
    public class ProductViewModel : IViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Unit { get; set; }
        public string LocaleName { get; set; }
    }
}
