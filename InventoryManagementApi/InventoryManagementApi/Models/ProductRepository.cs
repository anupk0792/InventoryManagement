using InventoryManagementApi.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryManagementApi.Models
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext appDbContext; // this will be used when connect to database
        public static List<Product> products = new List<Product>() 
        {
            new Product{ ProductId= 42154, Name="Product1", Category="Sale", ImageUrl = "api/image1", Description="Product1 description", Price=32400 },
            new Product{ ProductId= 24135, Name="Product2", Category="IT", ImageUrl = "api/image2", Description="Product2 description", Price=44000 },
            new Product{ ProductId= 12456, Name="Product3", Category="Software", ImageUrl = "api/image3", Description="Product3 description", Price=20000 }
        };

        public ProductRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        public Task<Product> AddProduct(Product product)
        {
            var result = products.FirstOrDefault(e => e.ProductId == product.ProductId);

            if (result == null)
            {
                products.Add(product);
            }
            return Task.FromResult(product);
        }

        public void DeleteProduct(int productId)
        {
            var result = products.FirstOrDefault(e => e.ProductId == productId);
            if (result != null)
            {
                products.Remove(result);
            }
        }

        public Task<IEnumerable<Product>> GetProducts()
        {
            return Task.FromResult<IEnumerable<Product>>(products);
        }

        public Task<Product> UpdateProduct(Product product)
        {
            var result = products.FirstOrDefault(e => e.ProductId == product.ProductId);
            if (result != null)
            {
                products.Where(e => e.ProductId == product.ProductId)
                    .Select(x => { x.ProductId = product.ProductId; x.Name = product.Name; x.Description = product.Description;
                        x.Category = product.Category; x.ImageUrl = product.ImageUrl; x.Price = product.Price; return x; }).ToList();
            }
            return Task.FromResult(result);
        }

        public Task<Product> GetProductById(int productId)
        {
            return Task.FromResult(products.Where(x=>x.ProductId== productId).FirstOrDefault());
        }
    }
}
