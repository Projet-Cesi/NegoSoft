using Microsoft.EntityFrameworkCore;
using NegoSoftShared.Models.Entities;
using NegoSoftShared.Models.ViewModels;
using NegoSoftWeb.Data;
using Stripe;
using Stripe.Climate;

namespace NegoAPI.Services.AlcoholProductService
{
    public class AlcoholProductService : IAlcoholProductService
    {
        private readonly NegoSoftContext _context;

        public AlcoholProductService(NegoSoftContext context)
        {
            _context = context;
        }
        public async Task<AlcoholProduct> CreateAlcoholProductAsync(AlcoholProductViewModel alcoholProduct)
        {
            if (alcoholProduct == null)
            {
                return null;
            }

            var newAlcoholProduct = new AlcoholProduct
            {
                ApId = Guid.NewGuid(),
                ApYear = alcoholProduct.ApYear,
                ApAlcoholVolume = alcoholProduct.ApAlcoholVolume,
                ProTypeId = alcoholProduct.ProTypeId
            };
            _context.AlcoholProducts.Add(newAlcoholProduct);
            await _context.SaveChangesAsync();
            return newAlcoholProduct;
        }

        public async Task<AlcoholProduct> DeleteAlcoholProductAsync(Guid id)
        {
            var alcoholProduct = await GetAlcoholProductByIdAsync(id);
            if (alcoholProduct == null)
            {
                return null;
            }
            _context.AlcoholProducts.Remove(alcoholProduct);
            await _context.SaveChangesAsync();
            return alcoholProduct;
        }

        public async Task<AlcoholProduct> GetAlcoholProductByIdAsync(Guid id)
        {
            return await _context.AlcoholProducts.FindAsync(id);
        }

        public async Task<IEnumerable<AlcoholProduct>> GetAllAlcoholProductsAsync()
        {
            return await _context.AlcoholProducts.ToListAsync(); 
        }

        public async Task<AlcoholProduct> UpdateAlcoholProductAsync(Guid id, AlcoholProductViewModel alcoholProduct)
        {
            var existingAlcoholProduct = await _context.AlcoholProducts.FindAsync(id);
            if (existingAlcoholProduct == null)
            {
                return null;
            }

            try
            {
                existingAlcoholProduct.ApYear = alcoholProduct.ApYear;
                existingAlcoholProduct.ApAlcoholVolume = alcoholProduct.ApAlcoholVolume;
                existingAlcoholProduct.ProTypeId = alcoholProduct.ProTypeId;
                await _context.SaveChangesAsync();
                return existingAlcoholProduct;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
