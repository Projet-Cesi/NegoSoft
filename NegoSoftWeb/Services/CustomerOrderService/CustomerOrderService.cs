using NegoSoftWeb.Services.AddressService;
using Stripe.Climate;
using NegoSoftWeb.Services.CartService;
using NegoSoftWeb.Services.CustomerService;
using NegoSoftWeb.Data;
using NegoSoftShared.Models.Entities;
using NegoSoftWeb.Services.CustomerOrderService;

namespace NegoSoftWeb.Services.CustomerOrderService
{
    public class CustomerOrderService : ICustomerOrderService
    {
        private readonly NegoSoftContext _context;
        private readonly ICustomerService _customerService;
        private readonly ICartService _cartService;
        private readonly IAddressService _addressService;

        public CustomerOrderService(NegoSoftContext context, ICustomerService customerService, ICartService cartService, IAddressService addressService)
        {
            _context = context;
            _customerService = customerService;
            _cartService = cartService;
            _addressService = addressService;
        }
        public async Task<CustomerOrder> CreateOrderAsync()
        {

            var customer = await _customerService.GetCustomerAsync();
            var cart = await _cartService.GetCartAsync();
            var address = await _addressService.GetAddressAsync();
            float total = 0;
            List<CustomerOrderDetails> orderDetails = new List<CustomerOrderDetails>();


            if (customer == null || cart == null || address == null)
            {
                throw new InvalidOperationException("Informations missing for the creation of the Order");
            }

            var newAddress = await _addressService.AddAddressAsync(address);
            var newCustomer = await _customerService.AddCustomerAsync(customer);
               

            var order = new CustomerOrder
            {
                CoId = Guid.NewGuid(),
                CoCustomerId = newCustomer.CusId,
                CoAddressId = newAddress.AddId,
                CoDate = DateTime.Now,
                CoState = "En attente",
                CoTotal = total
            };


            foreach (var item in cart)
            {
                total += item.ProPrice * item.ProQuantity;

                var orderDetail = new CustomerOrderDetails
                {
                    CodId = Guid.NewGuid(),
                    CodOrderId = order.CoId,
                    CodProductId = item.ProId,
                    CodQuantity = item.ProQuantity,
                    CodPrice = item.ProPrice * item.ProQuantity
                };
                orderDetails.Add(orderDetail);
            }

            order.CoTotal = total;

            order.CustomerOrderDetails = orderDetails;

            await _context.CustomerOrders.AddAsync(order);
            await _context.CustomerOrderDetails.AddRangeAsync(orderDetails);
            await _context.SaveChangesAsync();

            await _cartService.ClearCartAsync();

            return order;
        }
    }
}
