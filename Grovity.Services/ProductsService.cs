using Grovity.Database;
using Grovity.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
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

        private ProductsService()
        {
        }
        #endregion
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
            //int pageSize = 10;

            using (var context = new GrovityContext())
            {
                //return context.Products
                //    .OrderBy(x=>x.ID)
                //    .Skip((pageNo-1)* pageSize)
                //    .Take(pageSize)
                //    .Include(x=>x.Category).ToList();

                return context.Products.Include(x => x.Category).ToList();
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
