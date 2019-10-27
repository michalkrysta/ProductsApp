using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using ProductsApp.Core.Domain;
using ProductsApp.Core.Repositories;
using ProductsApp.Infrastructure.Exceptions;
using ProductsApp.Infrastructure.Services;
using Xunit;

namespace ProductsApp.Tests.Services
{
    public class ProductServiceTests
    {
        [Fact]
        public async Task add_product_should_invoke_add_async_on_repository_once()
        {
            var productRepositoryMock = new Mock<IProductsRepository>();
            var productService = new ProductService(productRepositoryMock.Object);

            await productService.AddProduct(It.IsAny<string>(), It.IsAny<decimal>(), It.IsAny<string>());

            productRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Product>()), Times.Once);
        }

        [Fact]
        public async Task add_product_should_throw_exception_if_product_already_exists_and_add_async_should_not_be_invoked()
        {
            var productRepositoryMock = new Mock<IProductsRepository>();
            productRepositoryMock.Setup(x => x.GetAsync(It.IsAny<int>())).ReturnsAsync(It.IsAny<Product>());
            productRepositoryMock.Setup(x => x.Find(It.IsAny<Expression<Func<Product, bool>>>()))
                .ReturnsAsync(new List<Product> {new Product()});

            var productService = new ProductService(productRepositoryMock.Object);

            Func<Task> act = async () => await productService.AddProduct(It.IsAny<string>(), It.IsAny<decimal>(), It.IsAny<string>());

            act.Should().Throw<ServiceException>().WithMessage("Product with Name: '' and Category: '' already exists.");
            productRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Product>()), Times.Never);
        }

        [Fact]
        public async Task browse_async_should_invoke_get_all_async_on_repository_once()
        {
            var productRepositoryMock = new Mock<IProductsRepository>();
            var productService = new ProductService(productRepositoryMock.Object);

            var sut = await productService.BrowseAsync();

            productRepositoryMock.Verify(x => x.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task get_async_should_invoke_get_async_on_repository_once()
        {
            var productRepositoryMock = new Mock<IProductsRepository>();
            productRepositoryMock.Setup(x => x.GetAsync(It.IsAny<int>())).ReturnsAsync(new Product());
            var productService = new ProductService(productRepositoryMock.Object);

            var sut = await productService.GetAsync(1);

            productRepositoryMock.Verify(x => x.GetAsync(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public async Task update_should_invoke_get_async_find_update_async_on_repository_once()
        {
            var productRepositoryMock = new Mock<IProductsRepository>();
            productRepositoryMock.Setup(x => x.GetAsync(It.IsAny<int>())).ReturnsAsync(new Product());
            productRepositoryMock.Setup(x => x.Find(It.IsAny<Expression<Func<Product, bool>>>())).ReturnsAsync(new List<Product>());

            var productService = new ProductService(productRepositoryMock.Object);
            await productService.Update(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<decimal>(), It.IsAny<string>());

            productRepositoryMock.Verify(x => x.GetAsync(It.IsAny<int>()), Times.Once);
            productRepositoryMock.Verify(x => x.Find(It.IsAny<Expression<Func<Product, bool>>>()), Times.Once);
            productRepositoryMock.Verify(x => x.UpdateAsync(It.IsAny<Product>()), Times.Once);
        }


        [Fact]
        public async Task update_should_throw_exception_when_product_with_given_id_not_exists_and_update_async_should_not_be_invoked()
        {
            var productRepositoryMock = new Mock<IProductsRepository>();
            productRepositoryMock.Setup(x => x.GetAsync(It.IsAny<int>())).ReturnsAsync((Product) null);

            var productService = new ProductService(productRepositoryMock.Object);

            Func<Task> act = async () => await productService.Update(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<decimal>(), It.IsAny<string>());

            act.Should().Throw<ServiceException>().WithMessage("Product with Id: '0' does not exist.");
            productRepositoryMock.Verify(x => x.UpdateAsync(It.IsAny<Product>()), Times.Never);
        }
    }
}