using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BookEcommerce.Models;
using BookEcommerce.DataAccess.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using BookEcommerce.DataAccess.Data;
using BookEcommerce.Utilities;
using Microsoft.AspNetCore.Http;

namespace BookEcommerce.Web.Areas.Customer.Controllers;

[Area("Customer")]
[Authorize]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ApplicationDbContext _context;

    public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork, ApplicationDbContext context)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _context = context;
    }

    public IActionResult Index()
    {
        var productList = _unitOfWork.Product.GetAll(null, new string[] { "Category" });
        return View(productList);
    }

    public IActionResult Details(int id)
    {
        ShoppingCart cart = new()
        {
            Product = _unitOfWork.Product.Get(p => p.Id == id, new string[] { "Category" }),
            Count = 1,
            ProductId = id,
        };
        return View(cart);
    }

    [HttpPost]
    [Authorize]
    public IActionResult Details(ShoppingCart shoppingCart)
    {
        var clainmsIdentity = (ClaimsIdentity)User.Identity;
        var userId = clainmsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
        shoppingCart.ApplicationUserId = userId;

        var cartFromDb = _unitOfWork.ShoppingCart.Get(c => c.ApplicationUserId == userId 
            && c.ProductId == shoppingCart.ProductId);

        if (cartFromDb == null) 
        {
            // no cart exists
            _unitOfWork.ShoppingCart.Add(shoppingCart);
            _unitOfWork.Save();

            HttpContext.Session.SetInt32(StaticDetails.SessionCart,
                _unitOfWork.ShoppingCart.GetAll(c => c.ApplicationUserId == userId).Count());
        }
        else
        {
            // cart exists
            cartFromDb.Count += shoppingCart.Count;
            _unitOfWork.ShoppingCart.Update(cartFromDb);
            _unitOfWork.Save();
        }

        TempData["success"] = "Cart updated successfully";

        return RedirectToAction(nameof(Index));
    }

    public IActionResult Privacy()
    {
        return View();
    }
}
