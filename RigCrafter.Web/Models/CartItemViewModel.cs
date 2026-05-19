using RigCrafter.DAL.Models;

namespace RigCrafter.Web.Models
{
    public class CartItemViewModel
    {
        public Product Product { get; set; } = null!;
        public int Quantity { get; set; }

        public decimal SubTotal => Product.Price * Quantity;
    }
}