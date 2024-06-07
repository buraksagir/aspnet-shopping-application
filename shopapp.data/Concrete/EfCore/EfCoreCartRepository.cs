using Microsoft.EntityFrameworkCore;
using shopapp.data.Abstract;
using shopapp.entity;
using System.Threading.Tasks;

namespace shopapp.data.Concrete.EfCore
{
    public class EfCoreCartRepository : EfCoreGenericRepository<Cart, ShopContext>, ICartRepository
    {
        public async Task<Cart> GetCartByUserIdAsync(string userId)
        {
            using (var context = new ShopContext())
            {
                return await context.Carts
                    .Include(c => c.CartItems)
                    .ThenInclude(ci => ci.Product)
                    .FirstOrDefaultAsync(c => c.UserId == userId);
            }
        }
        public async Task AddToCartAsync(string userId, int productId, int quantity)
        {
            using (var context = new ShopContext())
            {
                var cart = await context.Carts
                    .Include(c => c.CartItems)
                    .FirstOrDefaultAsync(c => c.UserId == userId);

                if (cart == null)
                {
                    cart = new Cart { UserId = userId };
                    context.Carts.Add(cart);
                }

                var cartItem = cart.CartItems.FirstOrDefault(ci => ci.ProductId == productId);
                if (cartItem != null)
                {
                    cartItem.Quantity += quantity;
                }
                else
                {
                    cart.CartItems.Add(new CartItem { ProductId = productId, Quantity = quantity });
                }

                await context.SaveChangesAsync();
            }
        }

    }
    
}