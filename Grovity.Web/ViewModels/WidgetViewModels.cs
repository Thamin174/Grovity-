using Grovity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Grovity.Web.ViewModels
{
    public class ProductsWidgetViewModels
    {
        public List<Product> Products { get; set; }
        public bool IsLatestProducts { get; set; }
    }
}