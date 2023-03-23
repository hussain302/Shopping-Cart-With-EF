using Microsoft.AspNetCore.Mvc;
using ShoppingCartInterfaces.IUnitOfWork;

namespace ShoppingCart.Areas.Admin.Controllers
{
    public class SettingsController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public SettingsController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> AppSettings()
        {
            try
            {
                int id = 1;
                var day =  await unitOfWork.Settings.GetTrendsDays(id);
                TempData["days"] = day.Days;
                return View();
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> TrendSettings([FromQuery] int days)
        {
            try
            {
                var res = await unitOfWork.Settings.UpdateTrendsDays(new ShoppingCartModels.DbModels.SetTrends { Id = 1, Days = days});
                if (Convert.ToBoolean(res)) TempData["Success"] = $"Trends set for {days} days";
                else throw new Exception("Didn't updated"); 
                return RedirectToAction("Dashboard", "Home");
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return View();
            }
        }
    }
}
