using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProductsApp.WebApp.Models;
using ProductsApp.WebApp.Models.Commands;

namespace ProductsApp.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HomeController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient("Products");
            var response = await client.GetAsync("api/Products");
            var products = new List<ProductViewModel>();

            if (!response.IsSuccessStatusCode)
                return View(products);

            products = await response.Content.ReadAsAsync<List<ProductViewModel>>();
            return View(products);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Cost,Category")] ProductViewModel productViewModel)
        {
            if (!ModelState.IsValid)
                return View(productViewModel);

            var command = new AddProductCommand
            {
                Name = productViewModel.Name,
                Cost = productViewModel.Cost,
                Category = productViewModel.Category
            };

            var payload = GetPayload(command);
            var client = _httpClientFactory.CreateClient("Products");
            var response = await client.PostAsync("api/Products", payload);

            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            ViewBag.Status = (await response.Content.ReadAsAsync<ResponseMessage>()).Message;

            return View(productViewModel);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var client = _httpClientFactory.CreateClient("Products");
            var response = await client.GetAsync($"api/Products/{id}");

            if (!response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            var product = await response.Content.ReadAsAsync<ProductViewModel>();
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Cost,Category")] ProductViewModel productViewModel)
        {
            if (!ModelState.IsValid)
                return View(productViewModel);

            var command = new UpdateProductCommand
            {
                Id = productViewModel.Id,
                Name = productViewModel.Name,
                Cost = productViewModel.Cost,
                Category = productViewModel.Category
            };

            var payload = GetPayload(command);
            var client = _httpClientFactory.CreateClient("Products");
            var response = await client.PutAsync("api/products", payload);

            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            ViewBag.Status = (await response.Content.ReadAsAsync<ResponseMessage>()).Message;
            return View(productViewModel);
        }


        protected static StringContent GetPayload(object data)
        {
            var json = JsonConvert.SerializeObject(data);
            return new StringContent(json, Encoding.UTF8, "application/json");
        }
    }
}