using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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
            TimeSpan ts = new TimeSpan();
            var day = await unitOfWork.Settings.GetTrendsDays(1);
            ts = ts.Subtract(TimeSpan.FromDays(day.Days));
            ViewData["Title"] = $"New Arrivals";
            ProductViewModel model = new ProductViewModel
            {
                Products = await Task.Run(() => unitOfWork.Product
                                          .GetProducts().Result.Where(x=>x.CreatedOn > DateTime.Now + ts)
                                          .ToList())
            };
            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IActionResult> ProductPage(string pageName)
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
    }
}
