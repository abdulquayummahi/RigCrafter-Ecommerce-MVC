using Microsoft.AspNetCore.Mvc;
using RigCrafter.BLL.Services;
using RigCrafter.DAL.Models;

namespace RigCrafter.Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public IActionResult MyOrders()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            var orders = _orderService.GetOrdersByUserId(userId.Value);

            return View(orders);
        }
    }
}