using shopapp.entity;
using System.Threading.Tasks;

namespace shopapp.data.Abstract
{
    public interface ICartRepository : IRepository<Cart>
    {
        Task<Cart> GetCartByUserIdAsync(string userId);
    }
}