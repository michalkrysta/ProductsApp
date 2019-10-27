using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProductsApp.Core.Domain;
using ProductsApp.Core.Repositories;
using ProductsApp.Infrastructure.Dto;
using ProductsApp.Infrastructure.Exceptions;

namespace ProductsApp.Infrastructure.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductsRepository _productsRepository;

        public ProductService(IProductsRepository productsRepository)
        {
            _productsRepository = productsRepository;
        }

        public async Task<IEnumerable<ProductDto>> BrowseAsync()
        {
            var products = await _productsRepository.GetAllAsync();
            return products.Select(x => new ProductDto
            {
                Id = x.Id,
                Name = x.Name,
                Cost = x.Cost,
                Category = x.Category
            });
        }

        public async Task<ProductDto> GetAsync(int id)
        {
            var product = await _productsRepository.GetAsync(id);
            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Cost = product.Cost,
                Category = product.Category
            };
        }

        public async Task AddProduct(string name, decimal cost, string category)
        {
            var product = await _productsRepository.Find(x => x.Name == name && x.Category == category);
            if (product.Any())
                throw new ServiceException(ErrorCodes.ProductAlreadyExists, $"Product with Name: '{name}' and Category: '{category}' already exists.");

            var productToAdd = new Product
            {
                Name = name,
                Cost = cost,
                Category = category
            };
            await _productsRepository.AddAsync(productToAdd);
        }

        public async Task Update(int productId, string productName, decimal productCost, string productCategory)
        {
            var product = await _productsRepository.GetAsync(productId);
            if (product == null)
                throw new ServiceException(ErrorCodes.ProductNotFound, $"Product with Id: '{productId}' does not exist.");

            var sameProduct = await _productsRepository.Find(x => x.Id != productId && x.Name == productName && x.Category == productCategory);
            if (sameProduct.Any())
                throw new ServiceException(ErrorCodes.ProductAlreadyExists, $"Product with Name: '{productName}' and Category: '{productCategory}' already exists.");

            product.Name = productName;
            product.Cost = productCost;
            product.Category = productCategory;

            await _productsRepository.UpdateAsync(product);
        }
    }
}