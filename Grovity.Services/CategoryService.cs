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
     public class CategoryService
    {
        #region Singleton
        public static CategoryService Instance
        {
            get
            {
                if (privateInMemoryObject == null) privateInMemoryObject = new CategoryService();

                return privateInMemoryObject;
            }
        }
        private static CategoryService privateInMemoryObject { get; set; }

        private CategoryService()
        {
        }
        #endregion
        public Category GetCategory(int ID)
        {
            using (var context = new GrovityContext())
            {
                return context.Categories.Find(ID);
            }
        }

        public int GetCategoriesCount(string search)
        {
            using (var context = new GrovityContext())
            {
                if (!string.IsNullOrEmpty(search))
                {
                    return context.Categories.Where(category => category.Name != null &&
                          category.Name.ToLower().Contains(search.ToLower())).Count();
                }
                else
                {
                    return context.Categories.Count();
                }
            }
        }
        public List<Category> GetCategories(string search, int pageNo)
        {
            int pageSize = 2;

            using (var context = new GrovityContext())
            {
                if (!string.IsNullOrEmpty(search))
                {

                   return context.Categories.Where(category => category.Name != null &&
                          category.Name.ToLower().Contains(search.ToLower()))
                          .OrderBy(x=>x.ID)
                          .Skip((pageNo-1)*pageSize)
                          .Take(pageSize)
                          .Include(x=>x.Products)
                          .ToList();
                }
                else
                {
                    return context.Categories
                   .OrderBy(x => x.ID)
                   .Skip((pageNo - 1) * pageSize)
                   .Take(pageSize).Include(x => x.Products)
                   .ToList();
                }
                
            }
        }
        public List<Category> GetAllCategories()
        {
            using (var context = new GrovityContext())
            {
                return context.Categories.ToList();
            }
        }
        public List<Category> GetFeaturedCategories()
        {
            using (var context = new GrovityContext())
            {
                return context.Categories.Where(x=>x.IsFeatured && x.ImageURL != null).ToList();
            }
        }


        public void SaveCategory(Category category)
        {
            using (var context = new GrovityContext())
            {
                context.Categories.Add(category);

                context.SaveChanges();
            }
        }
        public void UpdateCategory(Category category)
        {
            using (var context = new GrovityContext())
            {
                context.Entry(category).State = EntityState.Modified;

                context.SaveChanges();
            }
        }
        public void DeleteCategory(int ID)
        {
            using (var context = new GrovityContext())
            {
                var category = context.Categories.Find(ID);

                context.Categories.Remove(category);

                context.SaveChanges();
            }
        }
    }
}
