using Microsoft.AspNetCore.Mvc;
using ShoppingCartInterfaces.IUnitOfWork;
using ShoppingCartUtilities.WebUtils;

namespace ShoppingCart.Areas.Customer.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public HomeController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IActionResult> Index()
        {
            ViewBag.TopMen = await Task.Run(() => unitOfWork.Product.GetProducts().Result
                 .Where(x => x.Category.Name == ProductUtils.MenCategoryValue && x.IsTrending ==true).Take(3).ToList());

            ViewBag.TopWomen = await Task.Run(() => unitOfWork.Product.GetProducts().Result
                .Where(x => x.Category.Name == ProductUtils.WomenCategoryValue && x.IsTrending == true).Take(3).ToList());

            ViewBag.TopPerfumes = await Task.Run(() => unitOfWork.Product.GetProducts().Result
                .Where(x => x.Category.Name == ProductUtils.PerfumesCategoryValue && x.IsTrending == true).Take(3).ToList());

            ViewBag.TopFootwear = await Task.Run(() => unitOfWork.Product.GetProducts().Result
                .Where(x => x.Category.Name == ProductUtils.FootwearCategoryValue && x.IsTrending == true).Take(3).ToList());

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult About()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Contact()
        {
            return View();
        }
    }
}
