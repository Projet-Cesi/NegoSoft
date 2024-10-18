// ProductsController.cs
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NegoSoftShared.Models.Entities;
using NegoSoftWeb.Data;
using NegoSoftWeb.Models.ViewModels;
using NegoSoftWeb.Models.Entities;
using NegoSoftWeb.Services.ProductService;

namespace NegoSoftWeb.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly NegoSoftContext _context;

        public ProductController(IProductService productService, NegoSoftContext context)
        {
            _productService = productService;
            _context = context;
        }

        // GET: Product

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetAllProductsAsync();
            return View(products);
        }

        // GET: Product/Details/5

        [HttpGet]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _productService.GetProductByIdAsync(id.Value);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpGet]
        // GET: Product/Search
        public async Task<IActionResult> Search(string searchString, Guid? typeId, Guid? supplierId, int? productYear, SortOrder sortOrder = SortOrder.None)
        {
        
            var model = await _productService.SearchAsync(searchString, typeId, supplierId, productYear, sortOrder); 

            return View(model);
        }
    }
}