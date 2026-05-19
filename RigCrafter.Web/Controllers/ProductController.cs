using Microsoft.AspNetCore.Mvc;
using RigCrafter.BLL.Services;
using RigCrafter.Web.Models;

namespace RigCrafter.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public IActionResult Index(int? categoryId, string? searchString, decimal? maxPrice)
        {
            var viewModel = new CatalogViewModel
            {
                Products = _productService.GetFilteredCatalog(categoryId, searchString, maxPrice),
                Categories = _productService.GetAllCategories(),

                SelectedCategoryId = categoryId,
                SearchString = searchString,
                MaxPrice = maxPrice
            };

            return View(viewModel);
        }
    }
}