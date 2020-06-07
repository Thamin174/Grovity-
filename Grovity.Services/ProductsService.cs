﻿using Grovity.Database;
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
        public Product GetProduct(int ID)
        {
            using (var context = new GrovityContext())
            {
                return context.Products.Where(x => x.ID == ID).Include(x => x.Category).FirstOrDefault();
            }
        }
        public List<Product> GetProducts()
        {
            using (var context = new GrovityContext())
            {
                return context.Products.Include(x=>x.Category).ToList();
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