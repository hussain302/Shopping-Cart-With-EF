using Microsoft.AspNetCore.Mvc;
using ShoppingCartInterfaces.IUnitOfWork;
using ShoppingCartModels.ViewModels;

namespace ShoppingCart.Areas.Customer.Controllers
{
    public class ProductController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public ProductController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IActionResult> NewArivals()
        {
            try
            {
                ViewData["Title"] = $"Featured Products";
                ProductViewModel model = new ProductViewModel
                {
                    Products = await Task.Run(() => unitOfWork.Product
                                              .GetProducts().Result.OrderByDescending(x => x.CreatedOn)
                                              .ToList())
                };
                return View(model);
            }
            catch(Exception ex)
            {
                TempData["Error"] = $"{ex.Message}";
                return RedirectToAction("Index","Home");
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IActionResult> ProductPage(string pageName)
        {
            try
            {
                ViewData["Title"] = $"{pageName} Collection";
                ProductViewModel model = new ProductViewModel
                {
                    Products = await Task.Run(() => unitOfWork.Product
                                              .GetProducts().Result.Where(x => x.Category.Name == pageName)
                                              .ToList())
                };
                return View(model);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
