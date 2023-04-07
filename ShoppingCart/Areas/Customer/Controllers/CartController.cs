using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingCart.Common;
using ShoppingCartInterfaces.IUnitOfWork;
using ShoppingCartModels.DbModels;
using ShoppingCartModels.ViewModels;
using System;

namespace ShoppingCart.Areas.Customer.Controllers
{
    public class CartController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public CartController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult Cart()
        {
            ViewData["Title"] = "Cart";
            var cart = HttpContext.Session.GetObject<CartViewModel>("Cart") ?? new CartViewModel { Products = new List<Product>(), TotalPrice = 0 };
            return View(cart);
        }

        [HttpGet]
        public async Task<IActionResult> AddToCart(int productId/*, int quantity*/)
        {
            try
            {
                var product = await unitOfWork.Product.Find(productId);
                var cart = HttpContext.Session.GetObject<CartViewModel>("Cart") ?? new CartViewModel { Products = new List<Product>(), TotalPrice = 0 };
                cart.Products.Add(product);
                cart.TotalPrice += (product.Price);//* quantity
                HttpContext.Session.SetObject("Cart", cart);
                TempData["Success"] = $"{product.Name} Added to your cart";
                return RedirectToAction("Cart");
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"{ex.Message}";
                return RedirectToAction("Cart");
            }
        }


        [HttpGet]
        public async Task<IActionResult> RemoveItem(int productId/*, int quantity*/)
        {
            try
            {
                var product = await unitOfWork.Product.Find(productId);
                var cart = HttpContext.Session.GetObject<CartViewModel>("Cart") ?? new CartViewModel { Products = new List<Product>(), TotalPrice = 0 };
                int index = cart.Products.FindIndex(x=>x.Id == productId);
                cart.Products.RemoveAt(index);
                cart.TotalPrice -= (product.Price);//* quantity
                HttpContext.Session.SetObject("Cart", cart);
                TempData["Success"] = $"{product.Name} Removed from your cart";
                return RedirectToAction("Cart");
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"{ex.Message}";
                return RedirectToAction("Cart");
            }
        }


        [HttpGet]
        public IActionResult Checkout()
        {
            try
            {
                var cart = HttpContext.Session.GetObject<CartViewModel>("Cart") ?? new CartViewModel { Products = new List<Product>(), TotalPrice = 0 };
                var model = new Order
                {
                    OrderDate= DateTime.Now,
                    Total = cart.TotalPrice,                    
                };

                return View(model);
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"{ex.Message}";
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Order(Order order)
        {
            try
            {
                var cart = HttpContext.Session.GetObject<CartViewModel>("Cart") ?? new CartViewModel { Products = new List<Product>(), TotalPrice = 0 };

                double price = 0;
                var productTitles = string.Empty;
                Random rnd = new Random();
                int randomNumber, checkOrderId;

                do
                {
                    randomNumber = rnd.Next(1, 1000000000);
                    checkOrderId = unitOfWork.Order.Find(randomNumber).Result?.OrderId ?? 0;
                } while (randomNumber == checkOrderId);

                foreach (var product in cart.Products)
                {
                    price += product.Price;
                    productTitles += "\"" + product.Name + "\",";
                }

                int res = await unitOfWork.Order.Add(new Order 
                { 
                    OrderId = randomNumber,
                    ProductNames = productTitles,
                    Total = price,
                    OrderDate= DateTime.Now,
                    City = order.City,
                    OrderStatus = "pending",
                    CustomerEmail= order.CustomerEmail,
                    CustomerName= order.CustomerName,
                    Country=order.Country,
                    CustomerPhone= order.CustomerPhone,
                    PostalCode= order.PostalCode,
                    Province=order.Province,    
                    Street=order.Street,
                });

                HttpContext.Session.Remove("Cart");

                TempData["Success"] = $"Order has been successfully created\nYour TrackerID is {randomNumber}";

                return RedirectToAction("Index","Home");
            }
            catch (Exception ex)
            {
                HttpContext.Session.Remove("Cart");
                TempData["Error"] = $"{ex.Message}";
                return RedirectToAction("Cart");
            }
        }
    }
}
