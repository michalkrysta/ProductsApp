using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProductsApp.Infrastructure.Commands;

namespace ProductsApp.Api.Controllers
{
    [Route("api/[controller]")]
    public abstract class ApiBaseController : Controller
    {
        private readonly ICommandDispatcher _commandDispatcher;

        protected ApiBaseController(ICommandDispatcher commandDispatcher)
        {
            _commandDispatcher = commandDispatcher;
        }

        public IActionResult Index()
        {
            return View();
        }

        protected async Task DispatchAsync<T>(T command) where T : ICommand
        {
            await _commandDispatcher.DispatchAsync(command);
        }
    }
}