using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Persistence.Repository.v1;
using Presentation.Tests;
using System;
using System.Collections.Generic;
using WebApi.Controllers.v1;
using Xunit;

namespace HotDiggetyDogTests.Controllers.v1
{
    [Collection("Sequential")]
    public class ProductsControllerTests : DatabaseBaseTest
    {
        private readonly ProductsController _productsController;

        public ProductsControllerTests()
        {
            Repository<Product> productRepository = new(dataContext);
            _productsController = new ProductsController(productRepository);
        }

        [Fact]
        public async void GetProducts_ShouldReturn_OK()
        {
            // Act
            ActionResult<IEnumerable<Product>> actionResult = await _productsController.GetProducts();

            // Assert
            Assert.IsType<OkObjectResult>(actionResult.Result);
        }

        [Fact]
        public async void GetProductBy_Generated_Id_ShouldReturn_NotFound()
        {
            //Arrange
            Guid id = Guid.Parse("23737b93-4d76-4fc7-953d-0be0eae24786");

            // Act
            ActionResult<Product> actionResult = await _productsController.GetProductById(id);

            // Assert
            Assert.IsType<NotFoundObjectResult>(actionResult.Result);
        }

        [Fact]
        public async void GetProductById_ShouldReturn_Ok()
        {
            //Arrange
            Guid id = Guid.Parse("15a5c583-f1d5-444c-b142-8fccffcc394a");

            // Act
            ActionResult<Product> actionResult = await _productsController.GetProductById(id);

            // Assert
            Assert.IsType<OkObjectResult>(actionResult.Result);
        }

        [Fact]
        public async void Create_Null_Product_ShouldReturn_BadRequest()
        {
            //Arrange
            Product product = null;

            // Act
            ActionResult<Product> actionResult = await _productsController.CreateProduct(product);

            // Assert
            Assert.IsType<BadRequestObjectResult>(actionResult.Result);
        }

        [Fact]
        public async void Create_New_Product_ShouldReturn_CreatedAtAction()
        {
            //Arrange
            Product product = new()
            {
                Name = "Product",
                Description = "Product Description",
                Category = "Product Category",
                Price = 20
            };

            // Act
            ActionResult<Product> actionResult = await _productsController.CreateProduct(product);

            // Assert
            Assert.IsType<CreatedAtActionResult>(actionResult.Result);
        }

        [Fact]
        public async void Update_Null_Product_ShouldReturn_BadRequest()
        {
            //Arrange
            Product product = null;

            // Act
            ActionResult<Product> actionResult = await _productsController.UpdateProduct(product);

            // Assert
            Assert.IsType<BadRequestObjectResult>(actionResult.Result);
        }

        [Fact]
        public async void Remove_NonExisting_Product_ShouldReturn_BadRequest()
        {
            //Arrange
            Guid id = Guid.Parse("0e9b0951-0788-4498-a694-f50a916c56b5");

            // Act
            ActionResult<Product> actionResult = await _productsController.RemoveProduct(id);

            // Assert
            Assert.IsType<NotFoundObjectResult>(actionResult.Result);
        }

        [Fact]
        public async void RemoveProduct_ShouldReturn_NoContent()
        {
            //Arrange
            Guid id = Guid.Parse("e9440e2d-a0d8-4bf9-ad21-2d93ed664eef");

            // Act
            ActionResult<Product> actionResult = await _productsController.RemoveProduct(id);

            // Assert
            Assert.IsType<NoContentResult>(actionResult.Result);
        }
    }
}

