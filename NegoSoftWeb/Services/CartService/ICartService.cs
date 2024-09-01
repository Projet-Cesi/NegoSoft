using NegoSoftWeb.Models.Entities;

namespace NegoSoftWeb.Services.CartService
{
    public interface ICartService
    {
        Task<List<CartItem>> GetCartAsync();
        Task SaveCartAsync(List<CartItem> cart);
        Task AddToCartAsync(Guid id);
        Task RemoveFromCartAsync(Guid id);
    }
}
