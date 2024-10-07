using Microsoft.EntityFrameworkCore;
using NegoSoftShared.Models.Entities;
using NegoSoftWeb.Data;
using NegoSoftWeb.Models.Entities;
using NegoSoftWeb.Models.ViewModels;

namespace NegoAPI.Services.ProductService
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
                .ToListAsync();
        }

        public async Task<Product> GetProductByIdAsync(Guid id)
        {
            return await _context.Products
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
            _context.Entry(product).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<Product> DeleteProductAsync(Guid id)
        {
            var product = await GetProductByIdAsync(id);
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

    }
}
