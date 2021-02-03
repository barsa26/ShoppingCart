using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;
using ShoppingCart.Core.Models;

namespace ShoppingCart.DataAccess.InMemory
{
    public class ProductRepository
    {
        ObjectCache cache = MemoryCache.Default;
        List<Product> products;

        public ProductRepository()
        {
            products = cache["products"] as List<Product>;
            if (products == null)
                products = new List<Product>();
        }

        //Add products to cache
        public void Commit()
        {
            cache["products"] = products;
        }

        //Insert new product
        public void InsertProduct(Product prod)
        {
            products.Add(prod);
        }

        //Update existing product based on ID
        public void UpdateProduct(Product prod)
        {
            Product updateProd = products.Find(p => p.Id == prod.Id);

            if (updateProd != null)
                updateProd = prod;
            else
                throw new Exception("Product not found");

        }

        //Get product details by Product Id
        public Product GetProductById(string productId)
        {
            Product product = products.Find(p => p.Id == productId);

            if (product != null)
                return product;
            else
                throw new Exception("Product not found");

        }

        //Get list of products
        public IQueryable<Product> GetProductList()
        {
            return products.AsQueryable();
        }

        //Delete product by product Id
        public void DeleteProduct(string productId)
        {
            Product product = products.Find(p => p.Id == productId);
            if (product != null)
                products.Remove(product);
            else
                throw new Exception("Product not found");
        }
    }
}
