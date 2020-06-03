using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniInternetMagazin.Db;
using MiniInternetMagazin.Models.GroceryStoreViewModels;

namespace MiniInternetMagazin.Controllers
{
    public class CategoriesController : Controller
    {
        public IActionResult SelectCategory()
        {
            using (var context = new DataContext())
            {
                return View(context.Categories.ToList<Category>());
            }
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category category)
        {
            try
            {
                using (var context = new DataContext())
                {
                    context.Categories.Add(category);
                   if(context.SaveChanges()>0)
                    return RedirectToAction("SelectCategory");
                }
            }
            catch(Exception ex)
            {
                return View($"{ex.Message}");
            }
            return BadRequest("Не добавлен!");
        }
        [HttpGet]
        public ActionResult Delete()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Delete(int categoryId)
        {
            try
            {
                using (var context = new DataContext())
                {
                    var Categ = context.Categories.FirstOrDefault<Category>(p => p.CategoryId == categoryId);
                    if(Categ!=null)
                    {
                        context.Categories.Remove(Categ);
                    }
                    if (context.SaveChanges()>0)
                        return RedirectToAction("SelectCategory");
                }
            }
            catch(Exception ex)
            {
                return View($"{ex.Message}");
            }
            return BadRequest("Категория по такой Id не существует!");
        }
        [HttpGet]
        public ActionResult Update()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Update(Category category)
        {
            try
            {
                using (var context = new DataContext())
                { 
                    Category Categ = context.Categories.FirstOrDefault<Category>(p => p.CategoryId == category.CategoryId);
                    if(Categ!=null)
                    {
                        Categ.CategoryName = category.CategoryName;
                        context.Categories.Update(Categ);
                    }
                    if (context.SaveChanges() > 0)
                        return RedirectToAction("SelectCategory");
                }
            }
            catch (Exception ex)
            {
                return View($"{ex.Message}");
            }
            return BadRequest("Категория по такой Id не существует!");

        }
    }
}