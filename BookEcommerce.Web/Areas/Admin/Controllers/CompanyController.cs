using BookEcommerce.DataAccess.Repository.Interfaces;
using BookEcommerce.Models;
using BookEcommerce.Models.ViewModels;
using BookEcommerce.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookEcommerce.Web.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = StaticDetails.Role_Admin)]
public class CompanyController : Controller
{
    private readonly IUnitOfWork _unitOfWork;

    public CompanyController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public IActionResult Index()
    {
        var companies = _unitOfWork.Company.GetAll();
        return View(companies);
    }

    public IActionResult Upsert(int? id) 
    {
        if (id == null || id == 0)
        {
            // create
            return View(new Company());
        }
        else
        {
            var oldCompany = _unitOfWork.Company.Get(p => p.Id == id);
            return View(oldCompany);
        }
    }

    [HttpPost]
    public IActionResult Upsert(Company company)
    {
        if (company.Id == 0)
        {
            _unitOfWork.Company.Add(company);
        }
        else
        {
            _unitOfWork.Company.Update(company);
        }

        _unitOfWork.Save();

        TempData["success"] = "Company was created successfully";
        return RedirectToAction("Index");
    }

    #region API Calls

    [HttpGet]
    public IActionResult GetAll()
    {
        var companies = _unitOfWork.Company.GetAll();
        return Json(new {data = companies});
    }

    [HttpDelete]
    public IActionResult Delete(int? id)
    {
        var companyToBeDeleted = _unitOfWork.Product.Get(p => p.Id == id);
        if (companyToBeDeleted == null)
        {
            return Json(new { success = false, message = "The company was not found" });
        }

        _unitOfWork.Product.Remove(companyToBeDeleted);
        _unitOfWork.Save();

        return Json(new { success = true, message = "The company was deleted successfully" });
    }
    #endregion

}
