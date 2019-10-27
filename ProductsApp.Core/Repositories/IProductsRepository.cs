using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ProductsApp.Core.Domain;

namespace ProductsApp.Core.Repositories
{
    public interface IProductsRepository : IRepository
    {
        Task<Product> GetAsync(int id);
        Task<IEnumerable<Product>> GetAllAsync();
        Task AddAsync(Product user);
        Task UpdateAsync(Product user);
        Task<IEnumerable<Product>> Find(Expression<Func<Product, bool>> predicate);
    }
}