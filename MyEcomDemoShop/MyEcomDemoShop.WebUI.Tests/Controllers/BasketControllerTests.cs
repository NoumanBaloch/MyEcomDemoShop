using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyEcomDemoShop.Core.Contracts;
using MyEcomDemoShop.Core.Models;
using MyEcomDemoShop.Core.ViewModels;
using MyEcomDemoShop.Services;
using MyEcomDemoShop.WebUI.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MyEcomDemoShop.WebUI.Tests.Controllers
{
    [TestClass]
    public class BasketControllerTests
    {
        [TestMethod]
        public void CanAddBasketItem()
        {
            IRepository<Basket> baskets = new Mock.MockContext<Basket>();
            IRepository<Product> products = new Mock.MockContext<Product>();

            var httpContext = new Mock.MockHttpContext();

            IBasketService basketService = new BasketService(products, baskets);

            var controller = new BasketController(basketService);
            //basketService.AddtoBasket(httpContext, "1");
            
            controller.ControllerContext = new System.Web.Mvc.ControllerContext(httpContext, new System.Web.Routing.RouteData(), controller);

            controller.AddToBasket("1");
            Basket basket = baskets.Collection().FirstOrDefault();

            Assert.IsNotNull(basket);
            Assert.AreEqual(1, basket.BasketItems.Count);
            Assert.AreEqual("1", basket.BasketItems.ToList().FirstOrDefault().ProductId);
        }

        [TestMethod]
        public void CanGetSummaryViewModel()
        {
            IRepository<Basket> baskets = new Mock.MockContext<Basket>();
            IRepository<Product> products = new Mock.MockContext<Product>();

            products.Insert(new Product() { Id = "1", Price = 10 });
            products.Insert(new Product() { Id = "2", Price = 30 });

            Basket basket = new Basket();
            basket.BasketItems.Add(new BasketItem() { ProductId = "1", Quantity = 3 });
            basket.BasketItems.Add(new BasketItem() { ProductId = "2", Quantity = 5 });

            baskets.Insert(basket);

            IBasketService basketService = new BasketService(products, baskets);

            var controller = new BasketController(basketService);
            var httpContext = new Mock.MockHttpContext();
            httpContext.Request.Cookies.Add(new System.Web.HttpCookie("eCommerceBasket") { Value = basket.Id});

            controller.ControllerContext = new System.Web.Mvc.ControllerContext(httpContext, new System.Web.Routing.RouteData(), controller);

            var result = controller.BasketSummary() as PartialViewResult;
            var basketSummary = (BasketSummaryViewModel)result.ViewData.Model;

            Assert.AreEqual(8, basketSummary.BasketCount);
            Assert.AreEqual(180, basketSummary.BasketTotoal);
        }
    }
}
