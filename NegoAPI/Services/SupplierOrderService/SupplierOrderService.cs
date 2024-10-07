using Microsoft.EntityFrameworkCore;
using NegoSoftShared.Models.Entities;
using NegoSoftShared.Models.ViewModels;
using NegoSoftWeb.Data;

namespace NegoAPI.Services.SupplierOrderService
{
    public class SupplierOrderService : ISupplierOrderService
    {
        private readonly NegoSoftContext _context;

        public SupplierOrderService(NegoSoftContext context)
        {
            _context = context;
        }

        public async Task<SupplierOrder> CreateSupplierOrderAsync(SupplierOrderViewModel supplierOrder)
        {
            if (supplierOrder == null)
            {
                return null;
            }

            var newSupplierOrder = new SupplierOrder
            {
                SoId = Guid.NewGuid(),
                SoDate = supplierOrder.SoDate,
                SoTotal = supplierOrder.SoTotal,
                SoSupplierId = supplierOrder.SoSupplierId,
                SoAddressId = supplierOrder.SoAddressId,
                SoState = supplierOrder.SoState
            };
            _context.SupplierOrders.Add(newSupplierOrder);
            await _context.SaveChangesAsync();
            return newSupplierOrder;
        }

        public async Task<SupplierOrder> DeleteSupplierOrderAsync(Guid id)
        {
            var supplierOrder = await GetSupplierOrderByIdAsync(id);
            if (supplierOrder == null)
            {
                return null;
            }
            _context.SupplierOrders.Remove(supplierOrder);
            await _context.SaveChangesAsync();
            return supplierOrder;
        }

        public async Task<SupplierOrder> GetSupplierOrderByIdAsync(Guid id)
        {
            return await _context.SupplierOrders.FindAsync(id);
        }

        public async Task<IEnumerable<SupplierOrder>> GetAllSupplierOrdersAsync()
        {
            return await _context.SupplierOrders.ToListAsync();
        }

        public async Task<SupplierOrder> UpdateSupplierOrderAsync(Guid id, SupplierOrderViewModel supplierOrder)
        {
            var existingSupplierOrder = await _context.SupplierOrders.FindAsync(id);
            if (existingSupplierOrder == null)
            {
                return null;
            }
            try
            {
                existingSupplierOrder.SoDate = supplierOrder.SoDate;
                existingSupplierOrder.SoTotal = supplierOrder.SoTotal;
                existingSupplierOrder.SoSupplierId = supplierOrder.SoSupplierId;
                existingSupplierOrder.SoAddressId = supplierOrder.SoAddressId;
                existingSupplierOrder.SoState = supplierOrder.SoState;

                await _context.SaveChangesAsync();
                return existingSupplierOrder;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
    }
}
