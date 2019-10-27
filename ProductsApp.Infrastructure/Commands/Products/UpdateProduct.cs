namespace ProductsApp.Infrastructure.Commands.Products
{
    public class UpdateProduct : ICommand
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Cost { get; set; }
        public string Category { get; set; }
    }
}