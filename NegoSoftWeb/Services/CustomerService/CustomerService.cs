using NegoSoftWeb.Data;
using NegoSoftShared.Models.Entities;
using Microsoft.EntityFrameworkCore;
using NegoSoftWeb.Models.ViewModels;
using NegoSoftWeb.Models.Entities;
using NegoSoftWeb.Models.Extensions;

namespace NegoSoftWeb.Services.CustomerService
{
    public class CustomerService : ICustomerService
    {
        private const string CustomerSessionKey = "Customer";
        private readonly NegoSoftContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CustomerService(NegoSoftContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        // Méthode pour récupérer le client depuis la session
        public Task<CustomerViewModel> GetCustomerAsync()
        {
            var customer = _httpContextAccessor.HttpContext.Session.GetObjectFromJson<CustomerViewModel>(CustomerSessionKey);
            return Task.FromResult(customer);
        }

        public void SaveCustomer(CustomerViewModel customer)
        {
            _httpContextAccessor.HttpContext.Session.SetObjectAsJson(CustomerSessionKey, customer);
        }

        public async Task<Customer> GetCustomerByIdAsync(Guid id)
        {
            return await _context.Customers.FirstOrDefaultAsync(c => c.CusId == id);
        }

        public async Task AddCustomerAsync(CustomerViewModel customer)
        {
            var newCustomer = new Customer
            {
                CusId = Guid.NewGuid(),
                CusFirstName = customer.CusFirstName,
                CusLastName = customer.CusLastName,
                CusEmail = customer.CusEmail,
                CusPhone = customer.CusPhone,
            };
            await _context.Customers.AddAsync(newCustomer);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCustomerAsync(Customer customer)
        {
            _context.Customers.Update(customer);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCustomerAsync(Guid id)
        {
            var customer = await _context.Customers.FirstOrDefaultAsync(c => c.CusId == id);
            if (customer != null)
            {
                _context.Customers.Remove(customer);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> CustomerExists(Guid id)
        {
            return await _context.Customers.AnyAsync(c => c.CusId == id);
        }
    }
}
