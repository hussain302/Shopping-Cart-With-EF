using Microsoft.AspNetCore.Mvc;

namespace ShoppingCart.Areas.Admin.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
