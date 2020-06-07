using Grovity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Grovity.Web.ViewModels
{
    public class CheckoutViewModels
    {
        public List<Product> CartProducts{ get; set; }

        public List<int> CartProductsIDs { get; set; }
    }
}