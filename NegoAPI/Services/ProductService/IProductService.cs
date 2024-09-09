using Microsoft.EntityFrameworkCore.Infrastructure;
using NegoSoftShared.Models;

namespace NegoAPI.Services.ProductService
{
    public interface IProductService 
    {
        Task<IEnumerable<NegoSoftShared.Models.Entities.Product>> GetAllProductAsync();
    }
}
