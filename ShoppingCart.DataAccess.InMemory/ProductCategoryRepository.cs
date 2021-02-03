using ShoppingCart.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.DataAccess.InMemory
{
    public class ProductCategoryRepository
    {
        ObjectCache cache = MemoryCache.Default;
        List<ProductCategory> productCategories;

        public ProductCategoryRepository()
        {
            productCategories = cache["productCategories"] as List<ProductCategory>;
            if (productCategories == null)
                productCategories = new List<ProductCategory>();
        }

        //Add product categories to cache
        public void Commit()
        {
            cache["productCategories"] = productCategories;
        }

        //Insert new product category
        public void InsertProductCategory(ProductCategory category)
        {
            productCategories.Add(category);
        }

        //Update existing product category based on ID
        public void UpdateProductCategory(ProductCategory category)
        {
            ProductCategory updateProdCategory = productCategories.Find(p => p.Id == category.Id);

            if (updateProdCategory != null)
                updateProdCategory = category;
            else
                throw new Exception("Product category not found");

        }

        //Get product category by category Id
        public ProductCategory GetProductCategoryById(string categoryId)
        {
            ProductCategory productCategory = productCategories.Find(p => p.Id == categoryId);

            if (productCategory != null)
                return productCategory;
            else
                throw new Exception("Product category not found");

        }

        //Get list of categories
        public IQueryable<ProductCategory> GetProductCategoryList()
        {
            return productCategories.AsQueryable();
        }

        //Delete product category by category Id
        public void DeleteProductCategory(string categoryId)
        {
            ProductCategory category = productCategories.Find(p => p.Id == categoryId);
            if (category != null)
                productCategories.Remove(category);
            else
                throw new Exception("Product category not found");
        }
    }
}
