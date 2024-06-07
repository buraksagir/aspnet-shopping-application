using shopapp.data.Abstract;
using shopapp.entity;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace shopapp.data.Concrete.EfCore
{
    public class EfCoreOrderRepository : EfCoreGenericRepository<Order, ShopContext>, IOrderRepository
    {
        public async Task CreateOrderAsync(Order order)
        {
            using (var context = new ShopContext())
            {
                await context.Orders.AddAsync(order);
                await context.SaveChangesAsync();
            }
        }
    }
}