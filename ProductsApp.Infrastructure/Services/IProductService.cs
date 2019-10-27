using System.Collections.Generic;
using System.Threading.Tasks;
using ProductsApp.Infrastructure.Dto;

namespace ProductsApp.Infrastructure.Services
{
    public interface IProductService : IService
    {
        Task<IEnumerable<ProductDto>> BrowseAsync();
        Task<ProductDto> GetAsync(int id);
        Task AddProduct(string commandName, decimal commandCost, string commandCategory);
        Task Update(int productId, string productName, decimal productCost, string productCategory);
    }
}