using Microsoft.AspNetCore.Mvc;
using NegoAPI.Services.CustomerService;
using NegoSoftShared.Models.Entities;
using NegoSoftShared.Models.ViewModels;
using CustomerViewModel = NegoSoftShared.Models.ViewModels.CustomerViewModel;

namespace NegoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        // GET: api/customer
        [HttpGet]
        public async Task<ActionResult<List<Customer>>> GetCustomers()
        {
            var customers = await _customerService.GetAllCustomersAsync();
            return Ok(customers);
        }

        // GET: api/customer/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomer(Guid id)
        {
            var customer = await _customerService.GetCustomerByIdAsync(id);
            if (customer == null) return NotFound();
            return Ok(customer);
        }

        // POST: api/customer
        [HttpPost]
        public async Task<ActionResult<Customer>> CreateCustomer(CustomerViewModel customer)
        {
            var createdCustomer = await _customerService.CreateCustomerAsync(customer);
            return CreatedAtAction(nameof(GetCustomer), new { id = createdCustomer.CusId }, createdCustomer);
        }

        // PUT: api/customer/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomer(Guid id, CustomerViewModel customer)
        {
            if (customer == null)
            {
                return BadRequest();
            }
            try
            {
                await _customerService.UpdateCustomerAsync(id, customer);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return NoContent();
        }

        // DELETE: api/customer/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(Guid id)
        {
            try
            {
                await _customerService.DeleteCustomerAsync(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return NoContent();
        }
    }
}
