using NegoSoftShared.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using NegoAPI.Services.ProductService;

namespace NegoAPI.Controllers
{


    [ApiController]
    [Route("api/[controller]")]

    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProduct()
        {
            var products = await productService.GetAllProductAsync();

            if (products == null || !products.Any())
            {
                return NotFound("No products found.");
            }

            return Ok(products);
        }
    }
}
