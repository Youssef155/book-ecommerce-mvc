using BookEcommerce.DataAccess.Data;
using BookEcommerce.DataAccess.Repository;
using BookEcommerce.DataAccess.Repository.Interfaces;
using BookEcommerce.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookEcommerce.Web.Areas.Admin.Controllers;

[Area("Admin")]
public class CategoryController : Controller
{
    private readonly IUnitOfWork _unitOfWork;

    public CategoryController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public IActionResult Index()
    {
        List<Category> catList = _unitOfWork.Category.GetAll().ToList();
        return View(catList);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(Category category)
    {
        _unitOfWork.Category.Add(category);
        TempData["success"] = "Category is created successfully";
        return RedirectToAction("Index");
    }

    public IActionResult Edit(int? id)
    {
        if (id is null || id == 0)
            return NotFound();

        Category? category = _unitOfWork.Category.Get(c => c.Id == id);

        if (category is null)
            return NotFound();

        return View(category);
    }

    [HttpPost]
    public IActionResult Edit(Category category)
    {
        _unitOfWork.Category.Update(category);
        TempData["success"] = "Category is updated successfully";
        return RedirectToAction("Index");
    }

    public IActionResult Delete(int? id)
    {
        if (id is null || id == 0)
            return NotFound();

        Category? category = _unitOfWork.Category.Get(c => c.Id == id);

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

        Category? category = _unitOfWork.Category.Get(c => c.Id == id);

        if (category is null)
            return NotFound();

        _unitOfWork.Category.Remove(category);
        TempData["success"] = "Category is deleted successfully";
        return RedirectToAction("Index");
    }
}
