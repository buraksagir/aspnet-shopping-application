using shopapp.business.Abstract;
using shopapp.data.Abstract;
using shopapp.entity;
using System.Linq;
using System.Threading.Tasks;

namespace shopapp.business.Concrete
{
    public class CartManager : ICartService
    {
        private readonly ICartRepository _cartRepository;

        public CartManager(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public async Task<Cart> GetCartByUserIdAsync(string userId)
        {
            return await _cartRepository.GetCartByUserIdAsync(userId);
        }

        public async Task AddToCartAsync(string userId, int productId, int quantity)
        {
            var cart = await _cartRepository.GetCartByUserIdAsync(userId);
            if (cart != null)
            {
                var index = cart.CartItems.FindIndex(i => i.ProductId == productId);
                if (index < 0)
                {
                    cart.CartItems.Add(new CartItem()
                    {
                        ProductId = productId,
                        Quantity = quantity,
                        CartId = cart.Id
                    });
                }
                else
                {
                    cart.CartItems[index].Quantity += quantity;
                }
                 _cartRepository.Update(cart);
            }
        }

        public async Task RemoveFromCartAsync(string userId, int productId)
        {
            var cart = await _cartRepository.GetCartByUserIdAsync(userId);
            if (cart != null)
            {
                var cartItem = cart.CartItems.FirstOrDefault(i => i.ProductId == productId);
                if (cartItem != null)
                {
                    cart.CartItems.Remove(cartItem);
                     _cartRepository.Update(cart);
                }
            }
        }

        public async Task ClearCartAsync(string userId)
        {
            var cart = await _cartRepository.GetCartByUserIdAsync(userId);
            if (cart != null)
            {
                cart.CartItems.Clear();
                 _cartRepository.Update(cart);
            }
        }
    }
}
