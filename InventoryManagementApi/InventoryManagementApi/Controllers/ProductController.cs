using InventoryManagementApi.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryManagementApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository productRepository;

        public ProductController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        [HttpGet]
        [Route("getproducts")]
        public async Task<ActionResult> GetProducts()
        {
            try
            {
                return Ok(await productRepository.GetProducts());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error while fetching data.");
            }
        }

        [HttpGet]
        [Route("getproductbyId/{id:int}")]
        public async Task<ActionResult<Product>> GetProductById(int id)
        {
            try
            {
                var result = await productRepository.GetProductById(id);

                if (result == null) return NotFound();

                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        [HttpPost]
        [Route("addproduct")]
        public async Task<ActionResult<Product>> AddProduct([FromBody] Product product)
        {
            try
            {
                if (product == null)
                    return BadRequest();

                var addedProduct = await productRepository.AddProduct(product);

                return CreatedAtAction(nameof(GetProducts),
                    new { id = addedProduct.ProductId }, addedProduct);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error adding new product");
            }
        }

        [HttpPut]
        [Route("updateproduct")]
        public async Task<ActionResult<Product>> UpdateProduct([FromBody] Product product)
        {
            try
            {
                if (product == null)
                    return BadRequest();

                var updateProduct = await productRepository.GetProductById(product.ProductId);
                
                if (updateProduct == null)
                    return NotFound($"Product with Id = {product.ProductId} not found");

                return await productRepository.UpdateProduct(product);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating data");
            }
        }

        [HttpDelete]
        [Route("deleteproduct/{id:int}")]
        public async Task<ActionResult<Product>> DeleteProduct(int id)
        {
            try
            {
                var productToDelete = await productRepository.GetProductById(id);

                if (productToDelete == null)
                {
                    return NotFound($"Product with Id = {id} not found");
                }
                productRepository.DeleteProduct(id);

                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting data");
            }
        }
    }
}
