using Microsoft.AspNetCore.Mvc;
using ShoppingCartInterfaces.IUnitOfWork;

namespace ShoppingCart.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryController(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public async Task<IActionResult> Manage()
        {
            try
            {
                var categories = await _unitOfWork.Category.GetAll();
                TempData["Success"] = $"Categories Recived {categories.Count()}"; 
                return View(categories);
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Manage");
            }
        }
    }
}
