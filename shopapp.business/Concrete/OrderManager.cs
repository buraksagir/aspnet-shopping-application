using shopapp.business.Abstract;
using shopapp.data.Abstract;
using shopapp.entity;
using System.Threading.Tasks;

namespace shopapp.business.Concrete
{
    public class OrderManager : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderManager(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task CreateOrderAsync(Order order)
        {
            await _orderRepository.CreateOrderAsync(order);
        }
    }
}