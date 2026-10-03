using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC_OnlineStore.Data;
using MVC_OnlineStore.Models;

namespace MVC_OnlineStore.Controllers
{
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;

        public ProductController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index() {
            var products = _context.products.Include(p => p.Category).ToList();
            return View(products);
        
        }

        public IActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(
                _context.categories,
                "Id",
                "Name"
            );
            return View();
        }
        [HttpPost]
        public IActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                _context.products.Add(product);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(
                _context.categories,
                "Id",
                "Name",
                product.CategoryId
            );

            return View(product);
        }
        public IActionResult Edit(int id)
        {
            var product = _context.products.Find(id);

            if (product == null)
            {
                return NotFound();
            }

            ViewBag.CategoryId = new SelectList(
                _context.categories,
                "Id",
                "Name",
                product.CategoryId
            );

            return View(product);
        }
        [HttpPost]
        public IActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                _context.products.Update(product);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(
                _context.categories,
                "Id",
                "Name",
                product.CategoryId
            );

            return View(product);
        }
        public IActionResult Delete(int id)
        {
            var product = _context.products
                .Include(p => p.Category)
                .FirstOrDefault(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }
        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            var product = _context.products.Find(id);

            if (product != null)
            {
                _context.products.Remove(product);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}
