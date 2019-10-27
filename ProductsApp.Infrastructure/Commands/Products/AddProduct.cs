namespace ProductsApp.Infrastructure.Commands.Products
{
    public class AddProduct : ICommand
    {
        public string Name { get; set; }
        public decimal Cost { get; set; }
        public string Category { get; set; }
    }
}