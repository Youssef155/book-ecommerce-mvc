using BookEcommerce.DataAccess.Repository;
using BookEcommerce.DataAccess.Repository.Interfaces;
using BookEcommerce.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookEcommerce.Web.Areas.Admin.Controllers;

[Area("Admin")]
public class ProductController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    public ProductController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public IActionResult Index()
    {
        List<Product> products = _unitOfWork.Product.GetAll().ToList();
        return View(products);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(Product product)
    {
        _unitOfWork.Product.Add(product);
        TempData["success"] = "Product is created successfully";
        return RedirectToAction("Index");
    }

    public IActionResult Edit(int? id)
    {
        if (id is null || id == 0)
            return NotFound();

        Product? product = _unitOfWork.Product.Get(c => c.Id == id);

        if (product is null)
            return NotFound();

        return View(product);
    }

    [HttpPost]
    public IActionResult Edit(Product product)
    {
        _unitOfWork.Product.Update(product);
        TempData["success"] = "Product is updated successfully";
        return RedirectToAction("Index");
    }

    public IActionResult Delete(int? id)
    {
        if (id is null || id == 0)
            return NotFound();

        Product? product = _unitOfWork.Product.Get(c => c.Id == id);

        if (product is null)
            return NotFound();

        return View(product);
    }

    [HttpPost]
    [ActionName("Delete")]
    public IActionResult DeletePost(int? id)
    {
        if (id is null || id == 0)
            return NotFound();

        Product? product = _unitOfWork.Product.Get(c => c.Id == id);

        if (product is null)
            return NotFound();

        _unitOfWork.Product.Remove(product);
        TempData["success"] = "Product is deleted successfully";
        return RedirectToAction("Index");
    }
}
