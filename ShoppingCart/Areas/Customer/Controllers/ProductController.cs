using Microsoft.AspNetCore.Mvc;

namespace ShoppingCart.Areas.Customer.Controllers
{
    public class ProductController : Controller
    {

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult NewArivals()
        {
            return View();
        }
    }
}
