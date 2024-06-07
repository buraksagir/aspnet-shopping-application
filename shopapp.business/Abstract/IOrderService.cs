using shopapp.entity;
using System.Threading.Tasks;

namespace shopapp.business.Abstract
{
    public interface IOrderService
    {
        Task CreateOrderAsync(Order order);
    }
}