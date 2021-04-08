using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyEcomDemoShop.Core.Contracts;
using MyEcomDemoShop.Core.Models;
using MyEcomDemoShop.Core.ViewModels;
using MyEcomDemoShop.WebUI;
using MyEcomDemoShop.WebUI.Controllers;

namespace MyEcomDemoShop.WebUI.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void IndexPageDoesReturnProducts()
        {
            //Assert
            IRepository<Product> productContext = new Mock.MockContext<Product>();
            IRepository<ProductCategory> productCategoryContext = new Mock.MockContext<ProductCategory>();
            HomeController controller = new HomeController(productContext, productCategoryContext);

            //Act
            productContext.Insert(new Product());
            var result = controller.Index() as ViewResult;
            var viewModel = (ProductListViewModel)result.ViewData.Model;

            //Result
            Assert.AreEqual(1, viewModel.Products.Count());

        }
    }
}
