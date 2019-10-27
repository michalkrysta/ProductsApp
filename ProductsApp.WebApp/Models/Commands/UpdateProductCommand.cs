namespace ProductsApp.WebApp.Models.Commands
{
    public class UpdateProductCommand
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Cost { get; set; }
        public string Category { get; set; }
    }
}