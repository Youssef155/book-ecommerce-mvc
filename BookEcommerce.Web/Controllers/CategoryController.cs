using BookEcommerce.DataAccess.Data;
using BookEcommerce.DataAccess.Repository;
using BookEcommerce.DataAccess.Repository.Interfaces;
using BookEcommerce.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookEcommerce.Web.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryController(ICategoryRepository _categoryRepository)
        {
            this._categoryRepository = _categoryRepository;
        }

        public IActionResult Index()
        {
            List<Category> catList = _categoryRepository.GetAll().ToList();
            return View(catList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category category)
        {
            _categoryRepository.Add(category);
            TempData["success"] = "Category is created successfully";
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int? id)
        {
            if (id is null || id == 0)
                return NotFound();

            Category? category = _categoryRepository.Get(c => c.Id == id);

            if (category is null)
                return NotFound();

            return View(category);
        }

        [HttpPost]
        public IActionResult Edit(Category category)
        {
            _categoryRepository.Update(category);
            TempData["success"] = "Category is updated successfully";
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int? id)
        {
            if (id is null || id == 0)
                return NotFound();

            Category? category = _categoryRepository.Get(c => c.Id == id);

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

            Category? category = _categoryRepository.Get(c => c.Id == id);

            if (category is null)
                return NotFound();

            _categoryRepository.Remove(category);
            TempData["success"] = "Category is deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
