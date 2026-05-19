using Microsoft.AspNetCore.Mvc;
using RigCrafter.BLL.Services;
using RigCrafter.DAL.Models;

namespace RigCrafter.Web.Controllers
{
    public class AdminController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IProductService _productService;

        public AdminController(IOrderService orderService, IProductService productService)
        {
            _orderService = orderService;
            _productService = productService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var role = HttpContext.Session.GetString("UserRole");
            if (role != "Admin")
            {
                return RedirectToAction("Index", "Home");
            }

            var allOrders = _orderService.GetAllOrders();
            return View(allOrders);
        }

        [HttpPost]
        public IActionResult UpdateStatus(int orderId, string newStatus)
        {
            var role = HttpContext.Session.GetString("UserRole");
            if (role != "Admin") return RedirectToAction("Index", "Home");

            _orderService.UpdateOrderStatus(orderId, newStatus);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult ManageProducts()
        {
            if (HttpContext.Session.GetString("UserRole") != "Admin") return RedirectToAction("Index", "Home");
            var products = _productService.GetAllProducts();
            return View(products);
        }

        [HttpGet]
        public IActionResult AddProduct()
        {
            if (HttpContext.Session.GetString("UserRole") != "Admin") return RedirectToAction("Index", "Home");
            ViewBag.Categories = _productService.GetAllCategories();
            return View();
        }

        [HttpPost]
        public IActionResult AddProduct(Product newProduct)
        {
            if (HttpContext.Session.GetString("UserRole") != "Admin") return RedirectToAction("Index", "Home");

            _productService.AddProduct(newProduct);
            TempData["SuccessMessage"] = "Product added successfully!";
            return RedirectToAction("ManageProducts");
        }

        [HttpPost]
        public IActionResult DeleteProduct(int id)
        {
            if (HttpContext.Session.GetString("UserRole") != "Admin") return RedirectToAction("Index", "Home");

            _productService.DeleteProduct(id);
            TempData["SuccessMessage"] = "Product deleted successfully!";
            return RedirectToAction("ManageProducts");
        }
    }
}