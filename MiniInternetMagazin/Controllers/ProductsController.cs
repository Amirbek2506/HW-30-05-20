using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniInternetMagazin.Db;
using MiniInternetMagazin.Models.GroceryStoreViewModels;

namespace MiniInternetMagazin.Controllers
{
    public class ProductsController : Controller
    {
        DataContext _context { get; }
        public ProductsController(DataContext context)
        {
            _context = context;
        }
        public IActionResult SelectProductsCustomer(string category)
        {
            ViewBag.Categories = _context.Categories.ToList<Category>();
            var res = (from pr in _context.Products
                       join Categ in _context.Categories on pr.CategoryId equals Categ.CategoryId
                       select new ViewProduct { ProductName = pr.ProductName, ProductId = pr.ProductId, Price = pr.Price, Category = Categ.CategoryName }).ToList<ViewProduct>();

            if (category == null || category == "Все")
            {
                return View(res);
            }
            else
            {
                return View(res.Where(p => p.Category == category).ToList<ViewProduct>());
            }
        }
        public int Num { get; set; }
        [HttpGet]
        public IActionResult AddToBasket(int id)
        {
            ViewBag.Id = _context.Products.Single(p => p.ProductId == id).ProductId;
            return View();
        }
        [HttpPost]
        public IActionResult AddToBasket(Basket basket, int id)
        {
            basket.Product = _context.Products.Single(p => p.ProductId == id);
            basket.Id = 0;
            _context.Baskets.Add(basket);
            _context.SaveChanges();
            return RedirectToAction("Ok");
        }
        public IActionResult Ok()
        {
            return View("Ok");
        }
        public IActionResult Basket()
        { 
            var li = _context.Baskets.Include(p => p.Product).ToList();
                return View(li);
        }
        [HttpGet]
        public async Task<IActionResult> DeleteProductBasket(int id)
        {
            var model = _context.Baskets.FirstOrDefault(p => p.Id == id);
            _context.Baskets.Remove(model);
            await _context.SaveChangesAsync();
            return RedirectToAction("Basket");
        }
        public IActionResult SelectProducts(string category)
        {
            ViewBag.Categories = _context.Categories.ToList<Category>();
            var res = (from pr in _context.Products
                       join Categ in _context.Categories on pr.CategoryId equals Categ.CategoryId
                       select new ViewProduct { ProductName = pr.ProductName, ProductId = pr.ProductId, Price = pr.Price, Category = Categ.CategoryName }).ToList<ViewProduct>();

            if (category == null || category == "Все")
            {
                return View(res);
            }
            else
            {
                return View(res.Where(p => p.Category == category).ToList<ViewProduct>());
            }
        }
        public IActionResult Create()
        {
            ViewBag.Categories = _context.Categories.ToList<Category>();
            return View();
        }
        [HttpPost]
        public IActionResult Create(Product product)
        {
            try
            {
                _context.Products.Add(product);
                if (_context.SaveChanges() > 0)
                    return RedirectToAction("SelectProducts");
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
                var res = _context.Products.FirstOrDefault<Product>(p => p.ProductId == productId);
                if (res != null)
                {
                    _context.Products.Remove(res);
                }
                if (_context.SaveChanges() > 0)
                    return RedirectToAction("SelectProducts");
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
                Product res = _context.Products.FirstOrDefault<Product>(p => p.ProductId == product.ProductId);
                if (res != null)
                {
                    res.ProductName = product.ProductName;
                    res.Price = product.Price;
                    _context.Products.Update(res);
                }
                if (_context.SaveChanges() > 0)
                    return RedirectToAction("SelectProducts");
            }
            catch (Exception ex)
            {
                return View($"{ex.Message}");
            }
            return BadRequest("Продукт по такой Id не существует!");
        }

    }
}