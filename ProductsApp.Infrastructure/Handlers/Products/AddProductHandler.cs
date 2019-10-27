using System.Threading.Tasks;
using ProductsApp.Infrastructure.Commands;
using ProductsApp.Infrastructure.Commands.Products;
using ProductsApp.Infrastructure.Services;

namespace ProductsApp.Infrastructure.Handlers.Products
{
    public class AddProductHandler : ICommandHandler<AddProduct>
    {
        private readonly IProductService _productService;

        public AddProductHandler(IProductService productService)
        {
            _productService = productService;
        }

        public async Task HandleAsync(AddProduct command)
        {
            await _productService.AddProduct(command.Name, command.Cost, command.Category);
        }
    }
}