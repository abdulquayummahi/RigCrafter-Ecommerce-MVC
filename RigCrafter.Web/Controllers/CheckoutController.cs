using Microsoft.AspNetCore.Mvc;
using RigCrafter.BLL.Services;
using RigCrafter.DAL.Models;
using RigCrafter.Web.Helpers;
using RigCrafter.Web.Models;

namespace RigCrafter.Web.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IProductService _productService;

        public CheckoutController(IOrderService orderService, IProductService productService)
        {
            _orderService = orderService;
            _productService = productService; 
        }

        [HttpGet]
        public IActionResult Index()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                TempData["SuccessMessage"] = "Please log in to proceed to checkout.";
                return RedirectToAction("Login", "Auth");
            }
            
            var cart = HttpContext.Session.GetObjectFromJson<List<CartItemViewModel>>("ShoppingCart");
            if (cart == null || !cart.Any()) return RedirectToAction("Index", "Product");

            var viewModel = new CheckoutViewModel
            {
                CartItems = cart,
                GrandTotal = cart.Sum(i => i.SubTotal)
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult PlaceOrder(CheckoutViewModel model)
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<CartItemViewModel>>("ShoppingCart");
            var userId = HttpContext.Session.GetInt32("UserId");

            if (cart == null || !cart.Any()) 
                return RedirectToAction("Index", "Product");
            if (userId == null) 
                return RedirectToAction("Login", "Auth");

            foreach (var item in cart)
            {
                var dbProduct = _productService.GetProductById(item.Product.Id);
                if (dbProduct == null || dbProduct.StockQuantity < item.Quantity)
                {
                    TempData["ErrorMessage"] = $"Sorry, we only have {dbProduct?.StockQuantity ?? 0} of '{item.Product.Name}' left in stock. Please adjust your cart.";
                    return RedirectToAction("Index", "Cart");
                }
            }

            if (ModelState.IsValid)
            {
                var newOrder = new Order
                {
                    UserId = userId.Value,
                    ShippingAddress = model.ShippingAddress,
                    TotalAmount = cart.Sum(i => i.SubTotal),
                    OrderStatus = "Processing",
                    OrderDate = DateTime.Now
                };

                foreach (var item in cart)
                {
                    newOrder.OrderDetails.Add(new OrderDetail
                    {
                        ProductId = item.Product.Id,
                        Quantity = item.Quantity,
                        UnitPrice = item.Product.Price
                    });
                }

                _orderService.PlaceOrder(newOrder);

                HttpContext.Session.Remove("ShoppingCart");

                return View("OrderComplete");
            }

            model.CartItems = cart;
            model.GrandTotal = cart.Sum(i => i.SubTotal);
            return View("Index", model);
        }
    }
}