using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProductsApp.Core.Domain;
using ProductsApp.Core.Repositories;

namespace ProductsApp.Infrastructure.Repositories
{
    public class ProductRepository : IProductsRepository
    {
        private readonly DbContext _context;

        public ProductRepository(DbContext context)
        {
            _context = context;
        }

        public async Task<Product> GetAsync(int id)
        {
            return await _context.Set<Product>().SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Set<Product>().ToListAsync();
        }

        public async Task AddAsync(Product user)
        {
            _context.Set<Product>().Add(user);
            _context.SaveChanges();
            await Task.CompletedTask;
        }

        public async Task UpdateAsync(Product user)
        {
            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Product>> Find(Expression<Func<Product, bool>> predicate)
        {
            return await _context.Set<Product>().Where(predicate).ToArrayAsync();
        }
    }
}