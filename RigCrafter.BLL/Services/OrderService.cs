using RigCrafter.DAL;
using RigCrafter.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace RigCrafter.BLL.Services
{
    public class OrderService : IOrderService
    {
        private readonly RigCrafterDbContext _context;

        public OrderService(RigCrafterDbContext context)
        {
            _context = context;
        }

        public void PlaceOrder(Order newOrder)
        {
            foreach (var item in newOrder.OrderDetails)
            {
                var product = _context.Products.Find(item.ProductId);
                if (product != null)
                {
                    product.StockQuantity -= item.Quantity;
                }
            }

            _context.Orders.Add(newOrder);
            _context.SaveChanges();
        }

        public List<Order> GetOrdersByUserId(int userId)
        {
            return _context.Orders
                .Include(o => o.OrderDetails)
                .ThenInclude(od => od.Product)
                .Where(o => o.UserId == userId)
                .OrderByDescending(o => o.OrderDate)
                .ToList();
        }

        public List<Order> GetAllOrders()
        {
            return _context.Orders
                .Include(o => o.User)
                .Include(o => o.OrderDetails)
                .ThenInclude(od => od.Product)
                .OrderByDescending(o => o.OrderDate)
                .ToList();
        }
        public void UpdateOrderStatus(int orderId, string newStatus)
        {
            var order = _context.Orders.Find(orderId);
            if (order != null)
            {
                order.OrderStatus = newStatus;
                _context.SaveChanges();
            }
        }
    }
}