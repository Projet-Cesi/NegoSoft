using NegoSoftShared.Models.Entities;

namespace NegoSoftWeb.Services.CustomerOrderService
{
    public interface ICustomerOrderService
    {
        public Task<CustomerOrder> CreateCustomerOrderAsync();
        public Task<IEnumerable<CustomerOrder>> GetOrderHistoryByUserAsync(string userId);
        public Task<IEnumerable<CustomerOrderDetails>> GetOrderDetailsAsync(Guid orderId);
    }
}
