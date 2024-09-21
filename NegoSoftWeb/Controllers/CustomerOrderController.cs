using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NegoSoftWeb.Services.CustomerOrderService;
using NegoSoftWeb.Services.CustomerService;
using System.Security.Claims;

namespace NegoSoftWeb.Controllers
{
    public class CustomerOrderController : Controller
    {
        private readonly ICustomerOrderService _customerOrderService;
        private readonly ICustomerService _customerService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CustomerOrderController(ICustomerOrderService customerOrderService, ICustomerService customerService, IHttpContextAccessor httpContextAccessor)
        {
            _customerOrderService = customerOrderService;
            _customerService = customerService;
            _httpContextAccessor = httpContextAccessor;
        }

        // GET: CustomerOrder/History
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> History()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            if (userId == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var orders = await _customerOrderService.GetOrderHistoryByUserAsync(userId);

            return View(orders);
        }

        // GET: CustomerOrder/Details/5
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Details(Guid orderId)
        {
            var orderDetails = await _customerOrderService.GetOrderDetailsAsync(orderId);
            var count = orderDetails.Count();
            Console.WriteLine(count);

            if (orderDetails == null || !orderDetails.Any())
            {
                return NotFound();
            }

            return View(orderDetails);
        }

    }
}
