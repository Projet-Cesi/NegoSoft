
using NegoSoftWeb.Services.CartService;
using NegoSoftWeb.Services.CustomerOrderService;
using NegoSoftWeb.Services.ProductService;
using NegoSoftWeb.Services.SupplierOrderService;
using Stripe.Checkout;

namespace NegoSoftWeb.Services.PaymentsService
{
    public class PaymentsService : IPaymentsService
    {
        private readonly ICartService _cartService;
        private readonly ICustomerOrderService _customerOrderService;
        private readonly IProductService _productService;
        private readonly ISupplierOrderService _supplierOrderService;

        public PaymentsService(ICartService cartService, IProductService productService, ISupplierOrderService SupplierOrderService, ICustomerOrderService customerOrderService)
        {
            _cartService = cartService;
            _productService = productService;
            _supplierOrderService = SupplierOrderService;
            _customerOrderService = customerOrderService;

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
            try
            {
                await _supplierOrderService.CreateSupplierOrderAsync();
                await _customerOrderService.CreateCustomerOrderAsync();

                var cartItems = await _cartService.GetCartAsync();

                foreach (var item in cartItems)
                {
                    var product = await _productService.GetProductByIdAsync(item.ProId);
                    if (product != null)
                    {
                        int newStock = product.ProStock - item.ProQuantity;

                        if (newStock < 0)
                        {
                            product.ProStock = 0;
                        }
                        else
                        {
                            product.ProStock = newStock;
                        }

                        await _productService.UpdateProductAsync(product);
                        Console.WriteLine($"Product stock updated for product ID: {product.ProId}");

                        // Debugging: Check if the update was successful
                        var updatedProduct = await _productService.GetProductByIdAsync(item.ProId);
                        if (updatedProduct.ProStock != product.ProStock)
                        {
                            Console.WriteLine("Stock update did not succeed.");
                        }
                    }
                }

                // Clear the cart after processing all items
                await _cartService.ClearCartAsync();
            }
            catch (Exception ex)
            {
                // Handle exceptions or log them
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}
