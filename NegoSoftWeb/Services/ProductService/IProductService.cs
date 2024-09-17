using NegoSoftShared.Models.Entities;
using NegoSoftWeb.Models.ViewModels;

namespace NegoSoftWeb.Services.ProductService
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<Product> GetProductByIdAsync(Guid id);
        Task<Product> CreateProductAsync(Product product);
        Task<Product> UpdateProductAsync(Product product);
        Task<Product> DeleteProductAsync(Guid id);
        Task<bool> ProductExistsAsync(Guid id);
        Task<String> UploadFile(ProductViewModel product);
    }
}
