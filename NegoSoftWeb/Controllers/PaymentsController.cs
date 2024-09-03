using Microsoft.AspNetCore.Mvc;
using NegoSoftWeb.Services.CartService; // Service pour gérer le panier
using Stripe;
using Stripe.Checkout;
using System.Collections.Generic;
using System.Threading.Tasks;
using NegoSoftWeb.Models.ViewModels;
using NegoSoftWeb.Services.CustomerService;

namespace NegoSoftWeb.Controllers
{
    public class PaymentsController : Controller
    {
        private readonly ICartService _cartService;
        private readonly ICustomerService _customerService; // Service pour gérer les clients

        public PaymentsController(ICartService cartService, ICustomerService customerService)
        {
            _cartService = cartService;
            _customerService = customerService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCheckoutSession()
        {
            var domain = "https://localhost:7130/"; // Remplacez par votre domaine en production
            var items = await _cartService.GetCartAsync(); // Récupère les articles du panier

            if (items == null || items.Count == 0)
            {
                return BadRequest("Votre panier est vide.");
            }

            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },
                LineItems = new List<SessionLineItemOptions>(),
                Mode = "payment",
                SuccessUrl = domain + "Payments/Success",
                CancelUrl = domain + "Payments/Cancel",
            };

            long totalAmount = 0;

            foreach (var item in items)
            {
                var itemAmountInCents = (long)(item.ProPrice * 100);

                options.LineItems.Add(new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmount = itemAmountInCents,
                        Currency = "eur",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = item.ProName,
                        },
                    },
                    Quantity = item.ProQuantity,
                });

                totalAmount += itemAmountInCents * item.ProQuantity;
            }

            if (totalAmount < 50)
            {
                return BadRequest("Le montant total doit être d'au moins 0,50 €.");
            }

            var service = new SessionService();
            Session session = service.Create(options);

            return Json(new { id = session.Id });
        }

        public IActionResult Success()
        {
            return View();
        }

        public IActionResult Cancel()
        {
            return View();
        }
    }
}
