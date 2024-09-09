using Microsoft.EntityFrameworkCore.Infrastructure;
using NegoSoftShared.Models.Entities;

namespace NegoAPI.Services.ProductService
{
    public interface IProductService 
    {
        Task<IEnumerable<Product>> GetAllProductAsync();
    }
}
