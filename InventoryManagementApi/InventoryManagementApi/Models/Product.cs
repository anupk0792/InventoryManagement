using System;

namespace InventoryManagementApi
{
    public class Product
    {
        public int ProductId { get; set; }

        // product name 
        public string Name { get; set; }

        //product category
        public string Category { get; set; }

        //product description
        public string Description { get; set; }

        //product image url
        public string ImageUrl { get; set; }

        //product price
        public float Price { get; set; }

    }
}
