using InventoryManagementApi.Models;
using InventoryManagementApi.Interfaces;
using Moq;
using Xunit;
using System.Collections.Generic;
using InventoryManagementApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryManagementApi.Tests
{
    public class ProductControllerTests
    {

        private readonly Mock<IProductRepository> mockProductRepo;
        public ProductControllerTests()
        {
            mockProductRepo = new Mock<IProductRepository>();
        }

        [Fact]
        public void GetProducts_ListOfProducts_ProductsExistsInRepo()
        {
            //arrange
            var products = GetSampleProducts();
            mockProductRepo.Setup(x => x.GetProducts()).Returns(Task.FromResult<IEnumerable<Product>>(products));
            var controller = new ProductController(mockProductRepo.Object);

            //act
            var actionResult = controller.GetProducts();
            var result = actionResult.Result as OkObjectResult;
            var actual = result.Value as IEnumerable<Product>;

            //assert
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(GetSampleProducts().Count(), actual.Count());
        }

        [Fact]
        public void GetProductById_ProductDetailsById_ProductsExistsInRepo()
        {
            //arrange
            var products = GetSampleProducts();
            var firstProduct = products[0];
            mockProductRepo.Setup(x => x.GetProductById(42154)).Returns(Task.FromResult<Product>(firstProduct));
            var controller = new ProductController(mockProductRepo.Object);

            //act
            var result = controller.GetProductById(42154);

            //assert
            Assert.Equal(firstProduct, result.Result.Value);
        }

        [Fact]
        public void AddProduct_CreatedProduct_PassingProductObjectToCreate()
        {
            var products = GetSampleProducts();
            var newProduct = products[1];
            var controller = new ProductController(mockProductRepo.Object);
            var actionResult = controller.AddProduct(newProduct);
            var result = actionResult.Result;
            Assert.IsType<ActionResult<Product>>(result);

        }

        [Fact]
        public void UpdateProduct_UpdatedProduct_PassingProductObjectToUpdate()
        {
            var products = GetSampleProducts();
            var updateProduct = products[0];
            var controller = new ProductController(mockProductRepo.Object);
            var actionResult = controller.UpdateProduct(updateProduct);
            var result = actionResult.Result;
            Assert.IsType<ActionResult<Product>>(result);

        }

        private List<Product> GetSampleProducts()
        {
            List<Product> output = new List<Product>
            {
                new Product
                {
                    ProductId=42154,
                    Name= "Product1",
                    Description="Product1 Description",
                    Category="Sales",
                    ImageUrl= "api/image1",
                    Price=32400
                },
                new Product
                {
                    ProductId=24135,
                    Name= "Product2",
                    Description="Product2 Description",
                    Category="IT",
                    ImageUrl= "api/image2",
                    Price=44000
                },
                new Product
                {
                    ProductId=12456,
                    Name= "Product3",
                    Description="Product3 Description",
                    Category="Software",
                    ImageUrl= "api/image3",
                    Price=20000
                },
            };
            return output;
        }
    }
}
