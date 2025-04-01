using BookEcommerce.DataAccess.Data;
using BookEcommerce.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookEcommerce.Web.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _context;
        public CategoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            List<Category> catList = _context.Categories.ToList();
            return View(catList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
            TempData["success"] = "Category is created successfully";
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int? id)
        {
            if (id is null || id == 0)
                return NotFound();

            Category? category = _context.Categories.Find(id);

            if (category is null)
                return NotFound();

            return View(category);
        }

        [HttpPost]
        public IActionResult Edit(Category category)
        {
            _context.Categories.Update(category);
            _context.SaveChanges();
            TempData["success"] = "Category is updated successfully";
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int? id)
        {
            if (id is null || id == 0)
                return NotFound();

            Category? category = _context.Categories.Find(id);

            if (category is null)
                return NotFound();

            return View(category);
        }

        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            if (id is null || id == 0)
                return NotFound();

            Category? category = _context.Categories.Find(id);

            if (category is null)
                return NotFound();

            _context.Categories.Remove(category);
            _context.SaveChanges();
            TempData["success"] = "Category is deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
