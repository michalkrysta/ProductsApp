using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProductsApp.Api.Framework;
using ProductsApp.Infrastructure.Commands;
using ProductsApp.Infrastructure.Commands.Products;
using ProductsApp.Infrastructure.Services;

namespace ProductsApp.Api.Controllers
{
    [Log]
    public class ProductsController : ApiBaseController
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService,
            ICommandDispatcher commandDispatcher) : base(commandDispatcher)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var products = await _productService.BrowseAsync();

            return Json(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var user = await _productService.GetAsync(id);
            if (user == null) return NotFound();

            return Json(user);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AddProduct command)
        {
            await DispatchAsync(command);
            return Created($"products/{command.Name}", null);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdateProduct command)
        {
            await DispatchAsync(command);
            return NoContent();
        }
    }
}