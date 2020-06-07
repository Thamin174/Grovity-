using Grovity.Services;
using Grovity.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Grovity.Web.Controllers
{
    public class ShopController : Controller
    {
        ProductsService productsService = new ProductsService();
        // GET: Shop
        public ActionResult Checkout()
        {
            CheckoutViewModels model = new CheckoutViewModels();

            var CartProductsCookie = Request.Cookies["CartProducts"];

            if(CartProductsCookie != null)
            {

                model.CartProductsIDs = CartProductsCookie.Value.Split('-').Select(x => int.Parse(x)).ToList();

                model.CartProducts = productsService.GetProducts(model.CartProductsIDs);
            }
            return View(model);
        }
    }
}