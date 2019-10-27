namespace ProductsApp.WebApp.Models.Commands
{
    public class AddProductCommand
    {
        public string Name { get; set; }
        public decimal Cost { get; set; }
        public string Category { get; set; }
    }
}