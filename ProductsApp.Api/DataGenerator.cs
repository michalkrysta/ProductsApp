using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.DependencyInjection;
using ProductsApp.Core.Domain;
using ProductsApp.Infrastructure.EF;

namespace ProductsApp.Api
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new AppDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<AppDbContext>>()))
            {
                if (context.Products.Any()) return;

                context.Products.AddRange(
                    new Product {Name = "Lenovo ThinkPad P52", Cost = (decimal) 9000.00, Category = "Computers"},
                    new Product {Name = "Iphone Xr", Cost = (decimal) 3500.00, Category = "Mobile phones"},
                    new Product {Name = "MacBook Pro", Cost = (decimal) 7500, Category = "Computers"});

                context.SaveChanges();
            }
        }
    }
}