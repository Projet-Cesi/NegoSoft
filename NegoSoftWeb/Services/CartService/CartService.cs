using NegoSoftWeb.Models.Entities;
using NegoSoftWeb.Models.Extensions;
using NegoSoftWeb.Services.ProductService;


namespace NegoSoftWeb.Services.CartService
{
    public class CartService : ICartService
    {
        private const string CartSessionKey = "Cart"; // Clé de session pour le panier
        private readonly IProductService _productService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CartService(IProductService productService, IHttpContextAccessor httpContextAccessor)
        {
            _productService = productService;
            _httpContextAccessor = httpContextAccessor;
        }

        // Méthode pour obtenir le panier actuel à partir de la session 
        public Task<List<CartItem>> GetCartAsync()
        {
            var cart = _httpContextAccessor.HttpContext.Session.GetObjectFromJson<List<CartItem>>(CartSessionKey); // Récupère le panier de la session qui est stocké en JSON
            return Task.FromResult(cart ?? new List<CartItem>()); // Retourne le panier s'il existe, sinon retourne une nouvelle liste vide
        }

        // Méthode pour sauvegarder le panier dans la session
        public Task SaveCartAsync(List<CartItem> cart)
        {
            _httpContextAccessor.HttpContext.Session.SetObjectAsJson(CartSessionKey, cart); // Stocke le panier dans la session en le sérialisant en JSON
            return Task.CompletedTask; // Retourne une tâche terminée
        }

        // Action pour ajouter un produit au panier
        public async Task AddToCartAsync(Guid id, int quantity)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
            {
                throw new Exception("Produit non trouvé");
            }

            var cart = await GetCartAsync();
            var cartItem = cart.FirstOrDefault(c => c.ProId == product.ProId);

            if (quantity > product.ProStock)
            {
                throw new Exception("Quantité insuffisante en stock");
            }

            if (cartItem == null)
            {
                cart.Add(new CartItem
                {
                    ProId = product.ProId,
                    ProName = product.ProName,
                    ProPrice = product.ProPrice,
                    ProQuantity = quantity
                });
            }
            else
            {
                cartItem.ProQuantity += quantity;
            }

            await SaveCartAsync(cart);
        }

        // Action pour retirer un produit du panier
        public async Task RemoveFromCartAsync(Guid id)
        {
            var cart = await GetCartAsync();
            var cartItem = cart.FirstOrDefault(c => c.ProId == id);

            if (cartItem != null)
            {
                cart.Remove(cartItem);
                await SaveCartAsync(cart);
            }
        }
    }
}
