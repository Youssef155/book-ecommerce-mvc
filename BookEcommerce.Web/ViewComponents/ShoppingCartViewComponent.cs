using BookEcommerce.DataAccess.Repository.Interfaces;
using BookEcommerce.Utilities;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BookEcommerce.Web.ViewComponents
{
    public class ShoppingCartViewComponent : ViewComponent
    {
        private readonly IUnitOfWork _unitOfWork;
        public ShoppingCartViewComponent(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var clainmsIdentity = (ClaimsIdentity)User.Identity;
            var claim = clainmsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            if (claim != null)
            {
                if(HttpContext.Session.GetInt32(StaticDetails.SessionCart) == null)
                {
                    HttpContext.Session.SetInt32(StaticDetails.SessionCart,
                    _unitOfWork.ShoppingCart.GetAll(c => c.ApplicationUserId == claim.Value).Count());
                }
                
                return View(HttpContext.Session.GetInt32(StaticDetails.SessionCart));
            }
            else
            {
                HttpContext.Session.Clear();
                return View(0);
            }
        }
    }
}
