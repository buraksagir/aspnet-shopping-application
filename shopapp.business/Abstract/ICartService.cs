using shopapp.entity;
using System.Threading.Tasks;

namespace shopapp.business.Abstract
{
    public interface ICartService
    {
        Task<Cart> GetCartByUserIdAsync(string userId);
        Task AddToCartAsync(string userId, int productId, int quantity);
        Task RemoveFromCartAsync(string userId, int productId);
        Task ClearCartAsync(string userId);
    }
}