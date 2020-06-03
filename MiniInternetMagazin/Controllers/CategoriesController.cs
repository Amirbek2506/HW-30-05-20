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
        DataContext _context { get; }
        public CategoriesController(DataContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult SelectCategory()
        {
            return View(_context.Categories.ToList<Category>());
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
                _context.Categories.Add(category);
                if (_context.SaveChanges() > 0)
                    return RedirectToAction("SelectCategory");
            }
            catch (Exception ex)
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
                var Categ = _context.Categories.FirstOrDefault<Category>(p => p.CategoryId == categoryId);
                if (Categ != null)
                {
                    _context.Categories.Remove(Categ);
                }
                if (_context.SaveChanges() > 0)
                    return RedirectToAction("SelectCategory");
            }
            catch (Exception ex)
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
                Category Categ = _context.Categories.FirstOrDefault<Category>(p => p.CategoryId == category.CategoryId);
                if (Categ != null)
                {
                    Categ.CategoryName = category.CategoryName;
                    _context.Categories.Update(Categ);
                }
                if (_context.SaveChanges() > 0)
                    return RedirectToAction("SelectCategory");
            }
            catch (Exception ex)
            {
                return View($"{ex.Message}");
            }
            return BadRequest("Категория по такой Id не существует!");

        }
    }
}