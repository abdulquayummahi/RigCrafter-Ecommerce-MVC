using RigCrafter.DAL.Models;

namespace RigCrafter.BLL.Services
{
    public interface IOrderService
    {
        void PlaceOrder(Order newOrder);

        List<Order> GetOrdersByUserId(int userId);

        List<Order> GetAllOrders();
        void UpdateOrderStatus(int orderId, string newStatus);
    }
}