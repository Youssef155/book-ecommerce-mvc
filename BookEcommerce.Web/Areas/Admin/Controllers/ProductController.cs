﻿using BookEcommerce.DataAccess.Repository;
using BookEcommerce.DataAccess.Repository.Interfaces;
using BookEcommerce.Models;
using BookEcommerce.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookEcommerce.Web.Areas.Admin.Controllers;

[Area("Admin")]
public class ProductController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
    {
        _unitOfWork = unitOfWork;
        _webHostEnvironment = webHostEnvironment;
    }
    public IActionResult Index()
    {
        List<Product> products = _unitOfWork.Product.GetAll(new string[] { "Category" }).ToList();
        return View(products);
    }

    public IActionResult Upsert(int? id)
    {
        ProductVM productVM = new()
        {
            CategoryList = _unitOfWork.Category
            .GetAll().Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.Id.ToString()
            }),
            Product = new Product()
        };

        if(id == null || id == 0)
        {
            // create
            return View(productVM);
        }
        else
        {
            productVM.Product = _unitOfWork.Product.Get(p => p.Id == id);
            return View(productVM);
        }
    }

    [HttpPost]
    public IActionResult Upsert(ProductVM vm, IFormFile? file)
    {
        string wwwRootPath = _webHostEnvironment.WebRootPath;

        if (file != null)
        {
            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            string productPath = Path.Combine(wwwRootPath, @"images/products");

            if (!string.IsNullOrEmpty(vm.Product.ImgUrl))
            {
                // delete old image in case of update
                var oldImgPath = Path.Combine(wwwRootPath, vm.Product.ImgUrl.TrimStart('/'));

                if (System.IO.File.Exists(oldImgPath)) ;
                {
                    System.IO.File.Delete(oldImgPath);
                }
            }

            using(var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
            {
                file.CopyTo(fileStream);
            }

            vm.Product.ImgUrl = @"/images/products/" + fileName;
        }

        if (vm.Product.Id == 0)
        {
            _unitOfWork.Product.Add(vm.Product);
        }
        else
        {
            _unitOfWork.Product.Update(vm.Product);
        }

        TempData["success"] = "Product is created successfully";
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
