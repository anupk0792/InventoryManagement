using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryManagementApi.Interfaces
{
    public interface IProductRepository
    {
        // To get all products
        Task<IEnumerable<Product>> GetProducts();

        // To get product based on id
        Task<Product> GetProductById(int productId);

        // To add new product
        Task<Product> AddProduct(Product product);

        Task<Product> UpdateProduct(Product product);
        void DeleteProduct(int id);
    }
}
