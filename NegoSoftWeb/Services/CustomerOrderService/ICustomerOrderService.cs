using NegoSoftShared.Models.Entities;
using Stripe.Climate;

namespace NegoSoftWeb.Services.CustomerOrderService
{
    public interface ICustomerOrderService
    {
        public Task<CustomerOrder> CreateOrderAsync();
    }
}
