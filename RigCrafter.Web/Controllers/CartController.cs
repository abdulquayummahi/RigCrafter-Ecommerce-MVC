using Microsoft.AspNetCore.Mvc;
using RigCrafter.BLL.Services;
using RigCrafter.Web.Helpers;
using RigCrafter.Web.Models;

namespace RigCrafter.Web.Controllers
{
    public class CartController : Controller
    {
        private readonly IProductService _productService;

        public CartController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<CartItemViewModel>>("ShoppingCart")
                       ?? new List<CartItemViewModel>();

            return View(cart);
        }

        [HttpPost]
        public IActionResult AddToCart(int productId)
        {
            var product = _productService.GetProductById(productId);

            if (product == null) return NotFound();

            var cart = HttpContext.Session.GetObjectFromJson<List<CartItemViewModel>>("ShoppingCart")
                       ?? new List<CartItemViewModel>();

            var existingItem = cart.FirstOrDefault(c => c.Product.Id == productId);
            if (existingItem != null)
            {
                existingItem.Quantity++;
            }
            else
            {
                cart.Add(new CartItemViewModel { Product = product, Quantity = 1 });
            }

            HttpContext.Session.SetObjectAsJson("ShoppingCart", cart);

            TempData["SuccessMessage"] = $"{product.Name} was added to your cart!";

            return RedirectToAction("Index", "Product");
        }

        public IActionResult ClearCart()
        {
            HttpContext.Session.Remove("ShoppingCart");
            return RedirectToAction("Index");
        }
    }
}