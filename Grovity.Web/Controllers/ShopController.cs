using Grovity.Services;
using Grovity.Web.Code;
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

        public ActionResult Index(string searchTerm, int? minimumPrice, int? maximumPrice, int? categoryID, int? sortBy)
        {
            ShopViewModel model = new ShopViewModel();

            model.FeaturedCategories = CategoryService.Instance.GetFeaturedCategories();
            model.MaximumPrice = ProductsService.Instance.GetMaxiumPrice();

            model.Products = ProductsService.Instance.SearchProducts(searchTerm, minimumPrice, maximumPrice, categoryID, sortBy);

            model.sortBy = sortBy;

            return View(model);
        }
        // GET: Shop
        public ActionResult Checkout()
        {
            CheckoutViewModels model = new CheckoutViewModels();

            var CartProductsCookie = Request.Cookies["CartProducts"];

            if(CartProductsCookie != null)
            {

                model.CartProductsIDs = CartProductsCookie.Value.Split('-').Select(x => int.Parse(x)).ToList();

                model.CartProducts = ProductsService.Instance.GetProducts(model.CartProductsIDs);
            }
            return View(model);
        }
    }
}