using System.Threading.Tasks;
using ProductsApp.Infrastructure.Commands;
using ProductsApp.Infrastructure.Commands.Products;
using ProductsApp.Infrastructure.Services;

namespace ProductsApp.Infrastructure.Handlers.Products
{
    public class UpdateProductHandler : ICommandHandler<UpdateProduct>
    {
        private readonly IProductService _productService;

        public UpdateProductHandler(IProductService productService)
        {
            _productService = productService;
        }

        public async Task HandleAsync(UpdateProduct command)
        {
            await _productService.Update(command.Id, command.Name, command.Cost, command.Category);
        }
    }
}