using Microsoft.EntityFrameworkCore;
using NegoSoftShared.Models.Entities;
using NegoSoftWeb.Data;

namespace NegoAPI.Services.ProductService
{
    public class ProductService : IProductService
    {
        private readonly NegoSoftContext _context;
        public ProductService(NegoSoftContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAllProductAsync()
        {
            return await _context.Products.ToListAsync();
        }
    }
}
