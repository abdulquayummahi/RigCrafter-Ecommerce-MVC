using RigCrafter.DAL.Models;

namespace RigCrafter.Web.Models
{
    public class CatalogViewModel
    {
        public List<Product> Products { get; set; } = new List<Product>();
        public List<Category> Categories { get; set; } = new List<Category>();

        public int? SelectedCategoryId { get; set; }
        public string? SearchString { get; set; }
        public decimal? MaxPrice { get; set; }
    }
}