using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BookEcommerce.Models;
using BookEcommerce.DataAccess.Repository.Interfaces;

namespace BookEcommerce.Web.Areas.Customer.Controllers;

[Area("Customer")]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public IActionResult Index()
    {
        var productList = _unitOfWork.Product.GetAll(new string[] { "Category" });
        return View(productList);
    }

    public IActionResult Details(int id)
    {
        var product = _unitOfWork.Product.Get(p => p.Id == id, new string[] { "Category" });
        return View(product);
    }

    public IActionResult Privacy()
    {
        return View();
    }
}
