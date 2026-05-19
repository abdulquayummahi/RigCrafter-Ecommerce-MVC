using System.ComponentModel.DataAnnotations;

namespace RigCrafter.Web.Models
{
    public class CheckoutViewModel
    {
        [Required(ErrorMessage = "Please provide a shipping address.")]
        [Display(Name = "Shipping Address")]
        public string ShippingAddress { get; set; } = null!;

        public List<CartItemViewModel> CartItems { get; set; } = new List<CartItemViewModel>();
        public decimal GrandTotal { get; set; }
    }
}