using MyEcomDemoShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEcomDemoShop.DataAccess.SQL
{
    public class DataContext : DbContext
    {
        public DataContext() : base("myEcomShopDB")
        {
        }

        public DbSet<Product> Products { get; set; }
        public  DbSet<ProductCategory> ProductCategories { get; set; }
    }
}
