using MyEcomDemoShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace MyEcomDemoShop.DataAccess.InMemory
{
    public class ProductCategoryRepository
    {
        ObjectCache cache = MemoryCache.Default;
        List<ProductCategory> productCategoriess;

        public ProductCategoryRepository()
        {
            productCategoriess = cache["productCategoriess"] as List<ProductCategory>;
            if(productCategoriess == null)
            {
                productCategoriess = new List<ProductCategory>();
            }
        }

        public void Commit()
        {
            cache["productCategoriess"] = productCategoriess;
        }

        public void Insert(ProductCategory productCategory)
        {
            productCategoriess.Add(productCategory);
        }

        public void Update(ProductCategory productCategory)
        {
            ProductCategory productCategoryToUpdate = productCategoriess.Find(p => p.Id == productCategory.Id);

            if (productCategoryToUpdate != null)
            {
                productCategoryToUpdate = productCategory;
            }
            else
            {
                throw new Exception("Product Category not found");
            }
        }

        public ProductCategory Find(string Id)
        {
            ProductCategory productCategory = productCategoriess.Find(p => p.Id == Id);

            if(productCategory != null)
            {
                return productCategory;
            }
            else
            {
                throw new Exception("Product Category not found");
            }

        }

        public IQueryable<ProductCategory> Collection()
        {
            return productCategoriess.AsQueryable();
        }

        public void Delete(string Id)
        {
            ProductCategory productCategoryToDelete = productCategoriess.Find(p => p.Id == Id);
            
            if(productCategoryToDelete != null)
            {
                productCategoriess.Remove(productCategoryToDelete);
            }
            else
            {
                throw new Exception("Product Category not found");
            }
        }

    }
}
