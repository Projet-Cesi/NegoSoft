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

        // A supprimer car gérer plus tard dans le client lourd
        // GET: Product/Create
        public IActionResult Create()
        {
            ViewData["ProSupplierId"] = new SelectList(_context.Suppliers, "SupId", "SupName");
            ViewData["ProTypeId"] = new SelectList(_context.Types, "TypId", "TypLibelle");
            return View();
        }

        // A supprimer car gérer plus tard dans le client lourd
        // POST: Product/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductViewModel product)
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
            return RedirectToAction(nameof(Index));
        }



        // A supprimer car gérer plus tard dans le client lourd
        // GET: Product/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
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
            ViewData["ProSupplierId"] = new SelectList(_context.Suppliers, "SupId", "SupName", product.ProSupplierId);
            ViewData["ProTypeId"] = new SelectList(_context.Types, "TypId", "TypLibelle", product.ProTypeId);
            return View(product);
        }


        // A supprimer car gérer plus tard dans le client lourd
        // POST: Product/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Product product)
        {
            if (id != product.ProId)
            {
                return NotFound();
            }
            var oldProduct = await _productService.GetProductByIdAsync(id);
            product.ProPictureName = oldProduct.ProPictureName;
            try
            {
                await _productService.UpdateProductAsync(product);
            }
            catch (Exception)
            {
                if (!await _productService.ProductExistsAsync(product.ProId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            ViewData["ProSupplierId"] = new SelectList(_context.Suppliers, "SupId", "SupName", product.ProSupplierId);
            ViewData["ProTypeId"] = new SelectList(_context.Types, "TypId", "TypLibelle", product.ProTypeId);
            return RedirectToAction(nameof(Index)); 
        }


        // A supprimer car gérer plus tard dans le client lourd
        // GET: Product/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
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


        // A supprimer car gérer plus tard dans le client lourd
        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _productService.DeleteProductAsync(id);
            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        // GET: Product/Search
        public async Task<IActionResult> Search(string searchString, Guid? typeId, Guid? supplierId, Guid? alcoholProductId, SortOrder sortOrder = SortOrder.None)
        {
        
            var model = await _productService.SearchAsync(searchString, typeId, supplierId, alcoholProductId, sortOrder); 

            return View(model);
        }
    }
}