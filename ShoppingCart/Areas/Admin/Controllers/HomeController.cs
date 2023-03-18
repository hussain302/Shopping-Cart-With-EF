using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ShoppingCart.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        //[Authorize]
        public IActionResult Dashboard()
        {
            return View();
        }
    }
}