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
    public class ProductsController : Controller
    {
        public IActionResult SelectProducts(string category)
        {
            using (var context = new DataContext())
            {
                ViewBag.Categories = context.Categories.ToList<Category>();
                var res = (from pr in context.Products
                           join Categ in context.Categories on pr.CategoryId equals Categ.CategoryId
                           select new ViewProduct { ProductName = pr.ProductName, ProductId = pr.ProductId, Price = pr.Price, Category = Categ.CategoryName }).ToList<ViewProduct>();

                if (category == null || category=="Все")
                 {
                    return View(res);
                }
                else
                {
                    return View(res.Where(p => p.Category == category).ToList<ViewProduct>());
                }
            }
        }
        public IActionResult Create()
        {
            using (var context = new DataContext())
            {
                ViewBag.Categories = context.Categories.ToList<Category>();
                return View();
            }
        }

        [HttpPost]
        public IActionResult Create(Product product)
        {
            try
            {
                using (var context = new DataContext())
                {
                    context.Products.Add(product);
                    if (context.SaveChanges() > 0)
                        return RedirectToAction("SelectProducts");
                }
            }
            catch
            {
                return View();
            }
            return BadRequest("Не добавлен!");
        }
        [HttpGet]
        public ActionResult Delete()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Delete(int productId)
        {
            try
            {
                using (var context = new DataContext())
                {
                    var res = context.Products.FirstOrDefault<Product>(p => p.ProductId == productId);
                    if (res != null)
                    {
                        context.Products.Remove(res);
                    }
                    if (context.SaveChanges() > 0)
                        return RedirectToAction("SelectProducts");
                }
            }
            catch (Exception ex)
            {
                return View($"{ex.Message}");
            }
            return BadRequest("Продукт по такой Id не существует!");
        }
        [HttpGet]
        public ActionResult Update()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Update(Product product)
        {
            try
            {
                using (var context = new DataContext())
                {
                    Product res = context.Products.FirstOrDefault<Product>(p => p.ProductId == product.ProductId);
                    if (res != null)
                    {
                        res.ProductName = product.ProductName;
                        res.Price = product.Price;
                        context.Products.Update(res);
                    }
                    if (context.SaveChanges() > 0)
                        return RedirectToAction("SelectProducts");
                }
            }
            catch (Exception ex)
            {
                return View($"{ex.Message}");
            }
            return BadRequest("Продукт по такой Id не существует!");

        }

    }
}