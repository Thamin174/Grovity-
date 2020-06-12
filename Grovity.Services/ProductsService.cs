using Grovity.Database;
using Grovity.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace Grovity.Services
{
     public class ProductsService
    {
        #region Sigleton
        public static ProductsService Instance
        {
            get
            {
                if (privateInMemoryObject == null) privateInMemoryObject = new ProductsService();

                return privateInMemoryObject;
            }
        }
        private static ProductsService privateInMemoryObject { get; set; }

        public int GetMaxiumPrice()
        {
            using (var context = new GrovityContext())
            {
                return (int)(context.Products.Max(x => x.Price));
            }
        }

   

        private ProductsService()
        {
        }
        #endregion

        public List<Product> SearchProducts(string searchTerm, int? minimumPrice, int? maximumPrice, int? categoryID,int? sortBy)
        {

            using (var context = new GrovityContext())
            {
                var products = context.Products.ToList();

                if (categoryID.HasValue)
                {
                    products = products.Where(x => x.Category.ID == categoryID.Value).ToList();
                }
                if (!string.IsNullOrEmpty(searchTerm))
                {
                    products = products.Where(x => x.Name.ToLower().Contains(searchTerm.ToLower())).ToList();
                }
                if (minimumPrice.HasValue)
                {
                    products = products.Where(x => x.Price >= minimumPrice.Value).ToList();
                }
                if (maximumPrice.HasValue)
                {
                    products = products.Where(x => x.Price >= maximumPrice.Value).ToList();
                }
                if(sortBy.HasValue)
                {
                    switch (sortBy.Value)
                    {
                        case 2:
                            products = products.OrderByDescending(x => x.ID).ToList();
                            break;
                        case 3:
                            products = products.OrderBy(x => x.Price).ToList();
                            break;
                        default:
                            products = products.OrderByDescending(x => x.Price).ToList();
                            break;
                    }
                }
                return products;
            }
        }
        public Product GetProduct(int ID)
        {
            using (var context = new GrovityContext())
            {
                return context.Products.Where(x => x.ID == ID).Include(x => x.Category).FirstOrDefault();
            }
        }
        public List<Product> GetProducts(List<int> IDs)
        {
            using (var context = new GrovityContext())
            {
                return context.Products.Where(product => IDs.Contains(product.ID)).ToList();
            }
        }
        public List<Product> GetProducts(int pageNo)
        {
            int pageSize = 5;// int pageSize = int.Parse(ConfigurationService.Instance.GetConfig("ListingPageSize").Value);

            using (var context = new GrovityContext())
            {
                return context.Products
                    .OrderBy(x => x.ID)
                    .Skip((pageNo - 1) * pageSize)
                    .Take(pageSize)
                    .Include(x => x.Category).ToList();

                // return context.Products.Include(x => x.Category).ToList();
            }
        }

        public List<Product> GetProducts(int pageNo,int pageSize)
        {

            using (var context = new GrovityContext())
            {
                return context.Products
                    .OrderByDescending(x => x.ID)
                    .Skip((pageNo - 1) * pageSize)
                    .Take(pageSize)
                    .Include(x => x.Category).ToList();
            }
        }

        public List<Product> GetProductsByCategory(int categoryID,  int pageSize)
        {

            using (var context = new GrovityContext())
            {
                return context.Products
                    .Where(x=>x.Category.ID==categoryID)
                    .OrderByDescending(x => x.ID)
                    .Take(pageSize)
                    .Include(x => x.Category).ToList();
            }
        }

        public List<Product> GetLatestProducts(int numberOfProducts)
        {
            using (var context = new GrovityContext())
            {
                return context.Products
                    .OrderByDescending(x => x.ID)
                    .Take(numberOfProducts)
                    .Include(x => x.Category)
                    .ToList();
            }
        }


        public void SaveProduct(Product product)
        {
            using (var context = new GrovityContext())
            {
                context.Entry(product.Category).State = EntityState.Unchanged;

                context.Products.Add(product);

                context.SaveChanges();
            }
        }
        public void UpdateProduct(Product product)
        {
            using (var context = new GrovityContext())
            {
                context.Entry(product).State = EntityState.Modified;

                context.SaveChanges();
            }
        }
        public void DeleteProduct(int ID)
        {
            using (var context = new GrovityContext())
            {
                var product = context.Products.Find(ID);

                context.Products.Remove(product);

                context.SaveChanges();
            }
        }
    }
}
