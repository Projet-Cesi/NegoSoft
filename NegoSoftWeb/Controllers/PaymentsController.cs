using Microsoft.AspNetCore.Mvc;
using NegoSoftWeb.Services.CartService; // Service pour gérer le panier
using Stripe;
using Stripe.Checkout;
using System.Collections.Generic;
using System.Threading.Tasks;
using NegoSoftWeb.Models.ViewModels;
using NegoSoftWeb.Services.CustomerService;
using NegoSoftWeb.Services.CustomerOrderService;
using NegoSoftWeb.Services.PaymentsService;
using Stripe.Climate;
using Microsoft.AspNetCore.Authorization;

namespace NegoSoftWeb.Controllers
{
    public class PaymentsController : Controller
    {
        private readonly ICartService _cartService;
        private readonly ICustomerOrderService _orderService;
        private readonly ICustomerService _customerService;
        private readonly IPaymentsService _paymentsService;

        public PaymentsController(ICartService cartService, ICustomerOrderService orderService, ICustomerService customerService, IPaymentsService paymentsService)
        {
            _cartService = cartService;
            _orderService = orderService;
            _customerService = customerService;
            _paymentsService = paymentsService;
        }

        // GET: Payments

        [Authorize]
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        // POST: Payments/CreateCheckoutSession
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateCheckoutSession()
        {
            try
            {
                var sessionId = await _paymentsService.CreateCheckoutSessionAsync();
                return Json(new { id = sessionId });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: Payments/Success
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> SuccessAsync()
        {
            try
            {
                await _paymentsService.Success();
                return View();
            }
            catch (Exception ex)
            {
                return BadRequest("An error occurred while processing your payment.");
            }
        }

        // GET: Payments/Cancel
        [Authorize]
        [HttpGet]
        public IActionResult Cancel()
        {
            return View();
        }
    }
}
