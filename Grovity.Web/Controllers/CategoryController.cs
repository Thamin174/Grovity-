﻿using Grovity.Entities;
using Grovity.Services;
using Grovity.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Grovity.Web.Controllers
{
    public class CategoryController : Controller
    {
        CategoryService categoryService = new CategoryService();

        [HttpGet]
        public ActionResult Index()
        {
            var categories = categoryService.GetCategories();

            return View(categories);
        }
        public ActionResult CategoryTable(string search)
        {
            CategorySearchViewModel model = new CategorySearchViewModel();

            model.Categories = categoryService.GetCategories();

            if (!string.IsNullOrEmpty(search))
            {
                model.SearchTerm = search;

                model.Categories = model.Categories.Where(p => p.Name != null && p.Name.ToLower().Contains(search.ToLower())).ToList();
            }

            return PartialView("_CategoryTable", model);
        }

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
            var newCategory = new Category();
            newCategory.Name = model.Name;
            newCategory.Description = model.Description;
            newCategory.ImageURL = model.ImageURL;
            newCategory.IsFeatured = model.isFeatured;

            categoryService.SaveCategory(newCategory);

            return RedirectToAction("CategoryTable");
        }
        [HttpGet]
        public ActionResult Edit(int ID)
        {
            EditCategoryViewModel model = new EditCategoryViewModel();

            var category = categoryService.GetCategory(ID);
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

            var existingCategory = categoryService.GetCategory(model.ID);
            existingCategory.Name = model.Name;
            existingCategory.Description = model.Name;
            existingCategory.Description = model.Description;
            existingCategory.ImageURL = model.ImageURL;
            existingCategory.IsFeatured = model.isFeatured;

            categoryService.UpdateCategory(existingCategory);

            return RedirectToAction("CategoryTable");
        }
        
        [HttpPost]
        public ActionResult Delete(int ID)
        {

            categoryService.DeleteCategory(ID);

            return RedirectToAction("CategoryTable");
        }
    }
}