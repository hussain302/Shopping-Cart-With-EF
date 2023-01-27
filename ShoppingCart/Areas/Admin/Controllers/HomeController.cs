using Microsoft.AspNetCore.Mvc;

namespace ShoppingCart.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Dashboard()
        {
            return View();
        }
    }
}
