using Microsoft.AspNetCore.Mvc;
using ShoppingCartInterfaces.IUnitOfWork;
using ShoppingCartModels.ViewModels;

namespace ShoppingCart.Areas.Admin.Controllers
{
    public class OrderController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;

        public OrderController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Manage()
        {
            try
            {
                OrderViewModel model = new OrderViewModel
                {
                    Orders = await Task.Run(() => _unitOfWork.Order.Get().Result
                                     .OrderByDescending(x => x.OrderDate).ToList())
                };
                TempData["Success"] = "Orders fetched successfully";
                return View(model);
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Manage");                
            }
        }

        [HttpGet]
        public async Task<IActionResult> Details(int productId)
        {
            try
            {
                if(productId < 0) TempData["Error"] = "Wrong Product Id";
                
                OrderViewModel model = new OrderViewModel
                {
                    Order = await Task.Run(() => _unitOfWork.Order.Find(productId))
                };
                TempData["Success"] = "Order fetched successfully";
                return View(model);
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Manage");                
            }
        }


        [HttpPost]
        public async Task<IActionResult> UpdateOrder(OrderViewModel model)
        {
            try
            {
                if (model.Order.OrderStatus is null)
                {
                    TempData["Error"] = "Please select status";
                    return RedirectToAction("Details", model);
                }
                var response = await Task.Run(() => _unitOfWork.Order.Update(model.Order));
                if(response > 0) TempData["Success"] = "Order Updated successfully";
                else TempData["Error"] = "Order didn't Updated";
                return RedirectToAction(nameof(Manage));
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Manage");                
            }
        }
    }
}
