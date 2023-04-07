using Microsoft.AspNetCore.Mvc;
using ShoppingCartInterfaces.IUnitOfWork;
using ShoppingCartModels.DbModels;
using ShoppingCartModels.ViewModels;
using ShoppingCartRepository.UnitOfWork;
using ShoppingCartUtilities.WebUtils;

namespace ShoppingCart.Areas.Admin.Controllers
{
    public class UserController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public UserController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Manage()
        {
            try
            {
                UserViewModel model = new UserViewModel
                {
                    Users = await Task.Run(()=> _unitOfWork.User.Get().Result
                                                .OrderByDescending(x => x.IsApproved).ToList())
                };
                return View(model);
            }
            catch(Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Manage");
            }
        }

        [HttpGet]
        public IActionResult Login()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Login");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Login(AdminUser user)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(user.Username) || string.IsNullOrWhiteSpace(user.Password)) TempData["Error"] = "Username/Password Cant be empty";
                var findUser = await _unitOfWork.User.Login(user.Username, user.Password);
                if(findUser == null)
                {
                    TempData["Error"] = "User Not found";
                    return RedirectToAction("Login");
                }
                else if(findUser != null && findUser.IsApproved == false)
                {
                    TempData["Error"] = "User Not approved yet";
                    return RedirectToAction("Login");
                }
                TempData["Success"] = "User logged in successfully";                
                return RedirectToAction("Dashboard","Home");
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Login");
            }
        }

        [HttpGet]
        public IActionResult Register()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Register");
            }
        }
        [HttpPost]
        public async Task<IActionResult> Register(AdminUser user)
        {
            try
            {
                TempData["Error"] = FunctionForErrors.RegisterPageErrorValidation(user);
                if (TempData["Error"] != null) return RedirectToAction("Register");

                var resUser = await _unitOfWork.User.CreateUser(user);
                if (resUser == null)  TempData["Success"] = "User Created Successfully";
                else TempData["Error"] = "User Already Exists";
                return RedirectToAction("Login");
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Register");
            }
        }
    }
}
