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
    public class UsersController : Controller
    {
        DataContext _context { get; }
        public UsersController(DataContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Registration()
        {
            return View();
        }
        public IActionResult AddCustomer(User user)
        {
            if (user.Password != null)
            {
                if (user.Password.Length <= 3 || _context.Users.Where<User>(p => p.Login == user.Login || p.Password == user.Password).ToList<User>().Count<User>() != 0)
                    return RedirectToAction("Registration");
                else
                {
                    user.Roll = "Customer";
                    _context.Users.Add(user);
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Registration");
        }
        [HttpGet]
        public IActionResult FindUser(User user)
        {
            List<User> Us = _context.Users.Where<User>(p => p.Login == user.Login && p.Password == user.Password && p.Roll == user.Roll).ToList<User>();
            if (Us.Count > 0)
            {
                return RedirectToAction($"{user.Roll}");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        [HttpGet]
        public IActionResult Customer()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Admin()
        {
            return View();
        }
        // GET: User/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: User/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: User/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: User/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: User/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: User/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: User/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}