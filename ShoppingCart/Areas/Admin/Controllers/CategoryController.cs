using Microsoft.AspNetCore.Mvc;
using ShoppingCartInterfaces.IUnitOfWork;
using ShoppingCartModels.DbModels;
using ShoppingCartModels.ViewModels;
using System.Linq;

namespace ShoppingCart.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryController(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public IActionResult Manage()
        {
            try
            {
                CategoryViewModel model = new CategoryViewModel();
                model.Categories = _unitOfWork.Category.GetAll().Result.OrderBy(x => Convert.ToInt32(x.DisplayOrder));
                //TempData["Success"] = $"Categories Recived {model.Categories.Count()}"; 
                return View(model);
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Manage");
            }
        }
        [HttpGet]
        public async Task<IActionResult> CreateOrEdit(int? id)
        {
            try
            {
                //Create
                if(id == null)
                {
                    ViewData["Title"] = "Create";
                    ViewData["button-color"] = "btn btn-primary";
                    return View();
                }
                //Edit
                else
                {
                    ViewData["Title"] = "Update";
                    ViewData["button-color"] = "btn btn-success";
                    CategoryViewModel model = new CategoryViewModel();
                    model.Category = await _unitOfWork.Category.Find(Convert.ToInt32(id));
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Manage");
            }
        }
        [HttpPost]
        public async Task<IActionResult> CreateOrEdit(CategoryViewModel model)
        {
            try
            {
                int res = 0;
                //TempData["Success"] = $"{model.Category.Name} - Category added";
                if (model.Category.Id > 0)
                {
                    res = await _unitOfWork.Category.Update(model.Category);
                }
                else
                {
                    res = await _unitOfWork.Category.Add(model.Category);
                }
                if(res == 0) TempData["Error"] = $"{model.Category.Name} - Category didn't updated";               
                else TempData["Success"] = $"{model.Category.Name} - Category updated";               
                return RedirectToAction("Manage");
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Manage");
            }
        }
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                ViewData["Title"] = "Details";
                ViewData["button-color"] = "btn btn-warning";
                CategoryViewModel model = new CategoryViewModel();
                model.Category = await _unitOfWork.Category.Find(Convert.ToInt32(id));
                return View(model);
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Manage");
            }
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                ViewData["Title"] = "Delete";
                ViewData["button-color"] = "btn btn-danger";
                CategoryViewModel model = new CategoryViewModel();
                model.Category = await _unitOfWork.Category.Find(Convert.ToInt32(id));
                return View(model);

            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Manage");
            }
        }
        [HttpPost]
        public async Task<IActionResult> Delete(CategoryViewModel model)
        {
            try
            {
                int res = await _unitOfWork.Category.Delete(model.Category);
                if (res == 0) TempData["Error"] = $"{model.Category.Name} - Category didn't Deleted";
                else TempData["Success"] = $"{model.Category.Name} - Category Deleted";
                return RedirectToAction("Manage");
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Manage");
            }
        }
    }
}
