using BookEcommerce.Web.Data;
using BookEcommerce.Web.Models;
using Microsoft.AspNetCore.Mvc;

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
    }
}
