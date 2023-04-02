using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using ShoppingCartInterfaces.IUnitOfWork;
using ShoppingCartModels.DbModels;
using ShoppingCartModels.ViewModels;
using System.Web.WebPages.Html;

namespace ShoppingCart.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _web;
        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment web)
        {
            this._unitOfWork = unitOfWork;
            this._web = web;
        }
        public IActionResult Manage()
        {
            try
            {
                ProductViewModel model = new ProductViewModel();
                model.Products = _unitOfWork.Product.GetProducts()
                    .Result.OrderBy(x => Convert.ToInt32(x.DisplayOrder));
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
                ViewBag.Categories = await _unitOfWork.Category.GetAll();
                //Create
                if (id == null)
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
                    ProductViewModel model = new ProductViewModel
                    {
                        Product = await _unitOfWork.Product.Find(Convert.ToInt32(id))                        
                    };
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
        public async Task<IActionResult> CreateOrEdit(ProductViewModel model)
        {
            try
            {
                int res = 0;
                string folderPath = UploadedFile(model);
                model.Product.ImageUrl = folderPath;

                if (model.Product.Id > 0) res = await _unitOfWork.Product.Update(model.Product);
                else res = await _unitOfWork.Product.Add(model.Product);
                
                if (res == 0) TempData["Error"] = $"{model.Product.Name} - Product didn't updated";
                else TempData["Success"] = $"{model.Product.Name} - Product updated";
                
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
                ProductViewModel model = new ProductViewModel();
                model.Product = await _unitOfWork.Product.Find(Convert.ToInt32(id));
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
                ProductViewModel model = new ProductViewModel();
                model.Product = await _unitOfWork.Product.Find(Convert.ToInt32(id));
                return View(model);

            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Manage");
            }
        }
        [HttpPost]
        public async Task<IActionResult> Delete(ProductViewModel model)
        {
            try
            {
                int res = await _unitOfWork.Product.Delete(model.Product);
                if (res == 0) TempData["Error"] = $"{model.Product.Name} - Product didn't Deleted";
                else TempData["Success"] = $"{model.Product.Name} - Product Deleted";
                return RedirectToAction("Manage");
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Manage");
            }
        }

        #region Image Upload Logic
        private string UploadedFile(ProductViewModel model)
        {
            string uniqueFileName = string.Empty;

            if (model.PhotoFile != null)
            {
                string uploadsFolder = Path.Combine(_web.WebRootPath, "images/products");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.PhotoFile.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.PhotoFile.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
        #endregion

    }
}
