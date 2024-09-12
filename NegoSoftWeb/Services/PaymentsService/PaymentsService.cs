
using NegoSoftWeb.Services.CartService;
using NegoSoftWeb.Services.CustomerOrderService;
using NegoSoftWeb.Services.ProductService;
using Stripe.Checkout;

namespace NegoSoftWeb.Services.PaymentsService
{
    public class PaymentsService : IPaymentsService
    {
        private readonly ICartService _cartService;
        private readonly ICustomerOrderService _orderService;
        private readonly IProductService _productService;

        public PaymentsService(ICartService cartService, ICustomerOrderService orderService, IProductService productService)
        {
            _cartService = cartService;
            _orderService = orderService;
            _productService = productService;
        }
        public async Task<string> CreateCheckoutSessionAsync()
        {
            var domain = "https://localhost:7130/";
            var items = await _cartService.GetCartAsync(); // Récupère les articles du panier

            if (items == null || items.Count == 0)
            {
                throw new InvalidOperationException("Your cart is empty.");
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
                var product = await _productService.GetProductByIdAsync(item.ProId);
                if (product == null)
                {
                    throw new InvalidOperationException($"Product '{item.ProName}' does not exist.");
                }

                if (item.ProQuantity > product.ProStock)
                {
                    // c'est ici qu'il faudra instancier la commande fournisseur pour commander le produit en rupture de stock.
                    throw new InvalidOperationException($"Insufficient stock for product '{item.ProName}'. Available stock: {product.ProStock}, requested: {item.ProQuantity}");
                }
            }

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
                throw new InvalidOperationException("The total amount must be at least €0.50.");
            }

            var service = new SessionService();
            Session session = service.Create(options);

            return session.Id;
        }

        public async Task Success()
        {
            await _orderService.CreateOrderAsync();

            var cartItems = await _cartService.GetCartAsync();

            foreach (var item in cartItems)
            {
                var product = await _productService.GetProductByIdAsync(item.ProId);
                if (product != null)
                {
                    product.ProStock -= item.ProQuantity;
                    await _productService.UpdateProductAsync(product);
                }
            }

            await _cartService.ClearCartAsync();
        }
    }
}
