using Microsoft.EntityFrameworkCore;
using NegoSoftShared.Models.Entities;
using NegoSoftWeb.Data;

namespace NegoSoftWeb.Services.SupplierService
{
    public class SupplierService : ISupplierService
    {
        private readonly NegoSoftContext _context;

        public SupplierService(NegoSoftContext context)
        {
            _context = context;
        }

        public async Task<Supplier> GetSupplierByIdAsync(Guid id)
        {
            return await _context.Suppliers.AsNoTracking().FirstOrDefaultAsync(s => s.SupId == id);

        }
    }
}
