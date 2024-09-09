using NegoSoftShared.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using NegoAPI.Services.ProductService;
using NegoSoftWeb.Data;


namespace NegoAPI.Controllers
{


    [ApiController]
    [Route("[controller]")]

    public class ProductController : ControllerBase
    {

        private readonly NegoSoftContext dbContext;

        private readonly IProductService productService;

        public ProductController(NegoSoftContext dbContext, IProductService iproductService)
        {
            this.dbContext = dbContext;
            productService = iproductService;
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
