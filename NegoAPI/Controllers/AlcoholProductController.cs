using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NegoAPI.Services.AlcoholProductService;
using NegoAPI.Services.TypeService;
using NegoSoftShared.Models.Entities;
using NegoSoftShared.Models.ViewModels;
using System.Drawing.Drawing2D;

namespace NegoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlcoholProductController : ControllerBase
    {
        private readonly IAlcoholProductService _alcoholProductService;

        public AlcoholProductController(IAlcoholProductService alcoholProductService)
        {
            _alcoholProductService = alcoholProductService;
        }

        // GET: api/alcoholproduct
        [HttpGet]
        public async Task<ActionResult<List<AlcoholProduct>>> GetAlcoholProducts()
        {
            var alcoholProducts = await _alcoholProductService.GetAllAlcoholProductsAsync();
            return Ok(alcoholProducts);
        }

        // GET: api/alcoholproduct/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<AlcoholProduct>> GetAlcoholProduct(Guid id)
        {
            var alcoholProduct = await _alcoholProductService.GetAlcoholProductByIdAsync(id);
            if (alcoholProduct == null) return NotFound();
            return Ok(alcoholProduct);
        }

        // PUT: api/alcoholproduct/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAlcoholProduct(Guid id, AlcoholProductViewModel alcoholProduct)
        {
            if (alcoholProduct == null)
            {
                return BadRequest();
            }
            try
            {
                await _alcoholProductService.UpdateAlcoholProductAsync(id, alcoholProduct);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return NoContent();
        }

        // POST: api/alcoholproduct
        [HttpPost]
        public async Task<ActionResult<AlcoholProduct>> CreateAlcoholProduct(AlcoholProductViewModel alcoholProduct)
        {
            var createdAlcoholProduct = await _alcoholProductService.CreateAlcoholProductAsync(alcoholProduct);
            return CreatedAtAction(nameof(GetAlcoholProduct), new { id = createdAlcoholProduct.ApId }, createdAlcoholProduct);
        }

        // DELETE: api/alcoholproduct/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAlcoholProduct(Guid id)
        {
            try
            {
                await _alcoholProductService.DeleteAlcoholProductAsync(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return NoContent();
        }
    }
}

