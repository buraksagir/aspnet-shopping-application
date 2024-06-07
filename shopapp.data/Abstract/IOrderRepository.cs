using shopapp.entity;
using System.Threading.Tasks;

namespace shopapp.data.Abstract
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task CreateOrderAsync(Order order);
    }
}