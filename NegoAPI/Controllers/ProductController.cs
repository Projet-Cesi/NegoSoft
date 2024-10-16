using NegoSoftShared.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using NegoAPI.Services.ProductService;
using NegoSoftWeb.Models.ViewModels;
using NegoSoftWeb.Models.Entities;
using NegoSoftShared.Models.ViewModels;

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

        // GET: api/Products
        [HttpGet]
        public async Task<IActionResult> GetAllProduct()
        {
            var products = await _productService.GetAllProductsAsync();

            if (products == null || !products.Any())
            {
                return NotFound("No products found.");
            }

            return Ok(products);
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(Guid id)
        {
            var product = await _productService.GetProductByIdAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        // POST: api/Products
        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct([FromBody]NegoSoftWeb.Models.ViewModels.ProductViewModel product)
        {
            product.ProPictureName = await _productService.UploadFile(product);
            var newProduct = new Product
            {
                ProId = Guid.NewGuid(),
                ProName = product.ProName,
                ProDescription = product.ProDescription,
                ProPrice = product.ProPrice,
                ProBoxPrice = product.ProBoxPrice,
                ProStock = product.ProStock,
                ProPictureName = product.ProPictureName,
                ProTypeId = product.ProTypeId,
                ProSupplierId = product.ProSupplierId
            };

            await _productService.CreateProductAsync(newProduct);

            return CreatedAtAction(nameof(GetProduct), new { id = newProduct.ProId }, newProduct);
        }

        // PUT: api/Products/5
        [HttpPut("{id}")]
        public async Task<IActionResult> EditProduct(Guid id, ProductEditViewModel product)
        {
            var OldProduct = await _productService.GetProductByIdAsync(id);

            if (OldProduct == null)
            {
                return NotFound();
            }

            try
            {
                OldProduct.ProId = id;
                OldProduct.ProName = product.ProName;
                OldProduct.ProDescription = product.ProDescription;
                OldProduct.ProPrice = product.ProPrice;
                OldProduct.ProStock = product.ProStock;
                OldProduct.ProTypeId = product.ProTypeId;
                OldProduct.ProSupplierId = product.ProSupplierId;
                OldProduct.ProBoxPrice = product.ProBoxPrice;

                await _productService.UpdateProductAsync(OldProduct);
            }
            catch (Exception)
            {
                if (!await _productService.ProductExistsAsync(OldProduct.ProId))
                {
                    return NotFound();
                }
                else
                {
                    return StatusCode(500);
                }
            }

            return NoContent();
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            try
            {
                await _productService.DeleteProductAsync(id);
            }
            catch (Exception ex)
            {
                BadRequest(ex.Message);
            }

            return NoContent();
        }
    }
}
