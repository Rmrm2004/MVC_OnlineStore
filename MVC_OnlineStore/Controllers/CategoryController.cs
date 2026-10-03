using Microsoft.AspNetCore.Mvc;
using MVC_OnlineStore.Data;
using MVC_OnlineStore.Models;

namespace MVC_OnlineStore.Controllers
{
    public class CategoryController :Controller
    {
        private readonly AppDbContext _context;
        public CategoryController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index() {
            var categories = _context.categories.ToList();
            return View(categories);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                _context.categories.Add(category);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(category);
        }

        public IActionResult Edit(int id)
        {
            var category = _context.categories.Find(id);

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        public IActionResult Delete(int id)
        {
            var category = _context.categories.Find(id);

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            var category = _context.categories.Find(id);

            if (category != null)
            {
                _context.categories.Remove(category);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }





    }

}
