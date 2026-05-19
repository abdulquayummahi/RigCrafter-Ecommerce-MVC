using RigCrafter.DAL.Models;

namespace RigCrafter.BLL.Services
{
    public interface IProductService
    {
        List<Category> GetAllCategories();

        List<Product> GetFilteredCatalog(int? categoryId, string? searchString, decimal? maxPrice);

        List<Product> GetAllProducts();

        Product? GetProductById(int productId);

        void AddProduct(Product product);
        //void UpdateProduct(Product product);
        void DeleteProduct(int productId);
    }
}