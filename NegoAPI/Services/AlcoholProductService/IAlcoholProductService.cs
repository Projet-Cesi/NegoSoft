using NegoSoftShared.Models.Entities;
using NegoSoftShared.Models.ViewModels;

namespace NegoAPI.Services.AlcoholProductService
{
    public interface IAlcoholProductService
    {
        Task<AlcoholProduct> CreateAlcoholProductAsync(AlcoholProductViewModel alcoholProduct);
        Task<AlcoholProduct> UpdateAlcoholProductAsync(Guid id, AlcoholProductViewModel alcoholProduct);
        Task<AlcoholProduct> DeleteAlcoholProductAsync(Guid id);
        Task<IEnumerable<AlcoholProduct>> GetAllAlcoholProductsAsync();
        Task<AlcoholProduct> GetAlcoholProductByIdAsync(Guid id);
    }
}
