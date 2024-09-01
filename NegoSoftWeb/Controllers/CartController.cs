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
        public async Task<IActionResult> AddToCart(Guid id)
        {
            try
            {
                await _cartService.AddToCartAsync(id);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

            return RedirectToAction("Index");
        }

        // Action pour retirer un produit du panier
        public async Task<IActionResult> RemoveFromCart(Guid id)
        {
            await _cartService.RemoveFromCartAsync(id);
            return RedirectToAction("Index");
        }
    }
}
