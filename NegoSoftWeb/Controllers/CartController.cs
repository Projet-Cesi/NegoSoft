using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NegoSoftWeb.Models.Entities;
using NegoSoftWeb.Models.Extensions;
using NegoSoftWeb.Services.ProductService;

namespace NegoSoftWeb.Controllers
{
    public class CartController : Controller
    {
        private const string CartSessionKey = "Cart"; // Clé de session pour le panier qu'on va récupérer dans la méthode GetCartAsync
        private readonly IProductService _productService;

        public CartController(IProductService productService)
        {
            _productService = productService;
        }

        // Affiche le panier
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var cart = await GetCartAsync();
            return View(cart);
        }

        //  Action pour ajouter un produit au panier
        [HttpPost]
        public async Task<IActionResult> AddToCart(Guid id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            var cart = await GetCartAsync();
            var cartItem = cart.FirstOrDefault(c => c.ProId == product.ProId);

            if (cartItem == null)
            {
                cart.Add(new CartItem
                {
                    ProId = product.ProId,
                    ProName = product.ProName,
                    ProPrice = product.ProPrice,
                    ProQuantity = 1
                });
            }
            else
            {
                cartItem.ProQuantity++;
            }

            await SaveCartAsync(cart);

            return RedirectToAction("Index");
        }

        // Action pour retirer un film du panier
        public async Task<IActionResult> RemoveFromCart(Guid id)
        {
            var cart = await GetCartAsync();
            var cartItem = cart.FirstOrDefault(c => c.ProId == id);

            if (cartItem != null)
            {
                cart.Remove(cartItem);
                await SaveCartAsync(cart);
            }

            return RedirectToAction("Index");
        }

        // Méthode pour obtenir le panier actuel à partir de la session 
        private Task<List<CartItem>> GetCartAsync()
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>(CartSessionKey); // Récupère le panier de la session qui est stocké en JSON (les données de de la session sont stockées en JSON)
            return Task.FromResult(cart ?? new List<CartItem>()); // Retourne le panier s'il existe, sinon retourne une nouvelle liste vide
        }

        // Méthode pour sauvegarder le panier dans la session
        private Task SaveCartAsync(List<CartItem> cart)
        {
            HttpContext.Session.SetObjectAsJson(CartSessionKey, cart); // Stocke le panier dans la session en le sérialisant en JSON
            return Task.CompletedTask; // Retourne une tâche terminée
        }
    }
}
