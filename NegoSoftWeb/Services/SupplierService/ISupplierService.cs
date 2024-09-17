using NegoSoftShared.Models.Entities;

namespace NegoSoftWeb.Services.SupplierService
{
    public interface ISupplierService
    {
        Task<Supplier> GetSupplierByIdAsync(Guid id);
    }
}
