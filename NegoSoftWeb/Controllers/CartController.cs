using Microsoft.AspNetCore.Mvc;
using NegoSoftWeb.Services.CartService;

namespace NegoSoftWeb.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        // Affiche le panier
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var cart = await _cartService.GetCartAsync();
            return View(cart);
        }

        //  Action pour ajouter un produit au panier
        [HttpPost]
        public async Task<IActionResult> AddToCart(Guid id, int quantity)
        {
            try
            {
                await _cartService.AddToCartAsync(id, quantity);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

            return RedirectToAction("Index", "Product");
        }

        // Action pour retirer un produit du panier
        [HttpPost]
        public async Task<IActionResult> RemoveFromCart(Guid id, int quantity)
        {
            await _cartService.RemoveFromCartAsync(id, quantity);
            return RedirectToAction("Index");
        }

        // Action pour vider le panier
        [HttpPost]
        public async Task<IActionResult> ClearCart()
        {
            await _cartService.ClearCartAsync();
            return RedirectToAction("Index");
        }
    }
}
