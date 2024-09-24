using Microsoft.AspNetCore.Hosting;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using NegoSoftShared.Models.Entities;
using NegoSoftWeb.Data;
using NegoSoftWeb.Models.Entities;
using NegoSoftWeb.Models.ViewModels;


namespace NegoSoftWeb.Services.ProductService
{
    public class ProductService : IProductService
    {
        private readonly NegoSoftContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductService(NegoSoftContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _context.Products
                .Include(p => p.Supplier)
                .Include(p => p.Type)
                .ToListAsync();
        }

        public async Task<Product> GetProductByIdAsync(Guid id)
        {
            return await _context.Products
                .Include(p => p.Supplier)
                .Include(p => p.Type)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ProId == id);
        }

        public async Task<Product> CreateProductAsync(Product product)
        {
            product.ProId = Guid.NewGuid();
            product.ProPrice = (float)Math.Round(product.ProPrice, 2);
            _context.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<Product> UpdateProductAsync(Product product)
        {
            _context.Update(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<Product> DeleteProductAsync(Guid id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
            return product;
        }

        public async Task<bool> ProductExistsAsync(Guid id)
        {
            return await _context.Products.AnyAsync(e => e.ProId == id);
        }

        public async Task<String> UploadFile(ProductViewModel product)
        {
            if (product.ProImageFile != null)
            {
                // Get the path to the wwwroot folder
                string uploadFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
                Console.WriteLine(uploadFolder);

                // Create the folder if it doesn't exist
                if (!Directory.Exists(uploadFolder))
                {
                    Directory.CreateDirectory(uploadFolder);
                }

                // Generate a unique file name
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + product.ProImageFile.FileName;

                // Combine the folder path and the file name
                string filePath = Path.Combine(uploadFolder, uniqueFileName);

                // Sauvegarde le fichier sur le serveur
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await product.ProImageFile.CopyToAsync(fileStream);
                }

                product.ProPictureName = uniqueFileName;

            }
            return product.ProPictureName;

        }

        public async Task<ProductSearchViewModel> SearchAsync(string searchString, Guid? typeId, Guid? supplierId, Guid? alcoholProductId, SortOrder sortOrder)
        {
            var products = await GetAllProductsAsync();

            //controle de la recherche
            if (!String.IsNullOrEmpty(searchString))
            {
                products = products.Where(p => p.ProName.Contains(searchString, StringComparison.OrdinalIgnoreCase));
            }

            //on filtre par type de produit si un type est selectionné
            if (typeId.HasValue)
            {
                products = products.Where(p => p.ProTypeId == typeId.Value).ToList();
            }

            //on filtre par fournisseur si un fournisseur est selectionné
            if (supplierId.HasValue)
            {
                products = products.Where(p => p.ProSupplierId == supplierId.Value).ToList();
            }


            //on filtre par année si une année est selectionnée
            if (alcoholProductId.HasValue)
            {
                if (supplierId.HasValue && typeId.HasValue)
                {
                    products = (from p in _context.Products
                                join ap in _context.AlcoholProducts
                                on p.ProId equals ap.ProTypeId
                                where ap.ApId == alcoholProductId.Value
                                && p.ProSupplierId == supplierId.Value
                                && p.ProTypeId == typeId.Value
                                select p).ToList();
                }

                else if (supplierId.HasValue)
                {
                    products = (from p in _context.Products
                                join ap in _context.AlcoholProducts
                                on p.ProId equals ap.ProTypeId
                                where ap.ApId == alcoholProductId.Value
                                && p.ProSupplierId == supplierId.Value
                                select p).ToList();
                }
                else if (typeId.HasValue) 
                {
                    products = (from p in _context.Products
                                join ap in _context.AlcoholProducts
                                on p.ProId equals ap.ProTypeId
                                where ap.ApId == alcoholProductId.Value
                                && p.ProTypeId == typeId.Value
                                select p).ToList();
                }
                else
                {
                    products = (from p in _context.Products
                                join ap in _context.AlcoholProducts
                                on p.ProId equals ap.ProTypeId
                                where ap.ApId == alcoholProductId.Value
                                select p).ToList();
                }
            }

            //on trie les produits
            switch (sortOrder)
            {
                case SortOrder.NameAsc:
                    products = products.OrderBy(p => p.ProName).ToList();
                    break;
                case SortOrder.NameDesc:
                    products = products.OrderByDescending(p => p.ProName).ToList();
                    break;
                case SortOrder.PriceAsc:
                    products = products.OrderBy(p => p.ProPrice).ToList();
                    break;
                case SortOrder.PriceDesc:
                    products = products.OrderByDescending(p => p.ProPrice).ToList();
                    break;
            }

            //instanciation du model à passer à la vue contenant les produits filtrés et les types de produits
            var model = new ProductSearchViewModel
            {
                Products = products,
                ProductTypes = await _context.Types.ToListAsync(),
                SelectedTypeId = typeId,
                ProductSuppliers = await _context.Suppliers.ToListAsync(),
                SelectedSupplierId = supplierId,
                AlcoholProducts = await _context.AlcoholProducts.ToListAsync(),
                SelectedAlcoholProductId = alcoholProductId,
                SelectedSortOrder = sortOrder
            };

            return model;
        }
    }
}
