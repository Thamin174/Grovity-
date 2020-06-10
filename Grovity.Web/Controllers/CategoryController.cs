using Grovity.Entities;
using Grovity.Services;
using Grovity.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Data.Entity;

namespace Grovity.Web.Controllers
{
    public class CategoryController : Controller
    {
        

        [HttpGet]
        public ActionResult Index()
        {

            return View();
        }
        public ActionResult CategoryTable(string search, int? pageNo)
        {
            CategorySearchViewModel model = new CategorySearchViewModel();
            model.SearchTerm = search;

            pageNo = pageNo.HasValue ? pageNo.Value > 0 ? pageNo.Value : 1 : 1;

            var totalRecords = CategoryService.Instance.GetCategoriesCount(search);

           model.Categories = CategoryService.Instance.GetCategories(search, pageNo.Value);

            if (model.Categories != null)
            {


                model.Pager = new Pager(totalRecords, pageNo, 2);


                return PartialView("_CategoryTable", model);
            }
            else
            {
                return HttpNotFound();
            }
        }

        #region Creation
        // GET: Category
        [HttpGet]
        public ActionResult Create()
        {
            NewCategoryViewModel model = new NewCategoryViewModel();

            return PartialView(model);
        }

        
        [HttpPost]
        public ActionResult Create(NewCategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                var newCategory = new Category();
                newCategory.Name = model.Name;
                newCategory.Description = model.Description;
                newCategory.ImageURL = model.ImageURL;
                newCategory.IsFeatured = model.isFeatured;

                CategoryService.Instance.SaveCategory(newCategory);

                return RedirectToAction("CategoryTable");
            }
            else
            {
                return new HttpStatusCodeResult(500);
            }
        }
        #endregion

        #region Updation
        [HttpGet]
        public ActionResult Edit(int ID)
        {
            EditCategoryViewModel model = new EditCategoryViewModel();

            var category = CategoryService.Instance.GetCategory(ID);
            model.ID = category.ID;
            model.Name = category.Name;
            model.Description = category.Description;
            model.ImageURL = category.ImageURL;
            model.isFeatured = category.IsFeatured;

            return PartialView(model);
        }
        [HttpPost]
        public ActionResult Edit(EditCategoryViewModel model)
        {

            var existingCategory = CategoryService.Instance.GetCategory(model.ID);
            existingCategory.Name = model.Name;
            existingCategory.Description = model.Name;
            existingCategory.Description = model.Description;
            existingCategory.ImageURL = model.ImageURL;
            existingCategory.IsFeatured = model.isFeatured;

            CategoryService.Instance.UpdateCategory(existingCategory);

            return RedirectToAction("CategoryTable");
        }
        #endregion

        [HttpPost]
        public ActionResult Delete(int ID)
        {

            CategoryService.Instance.DeleteCategory(ID);

            return RedirectToAction("CategoryTable");
        }
    }
}