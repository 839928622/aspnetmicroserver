using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Mapster;
using MediatR;
using Ordering.Application.Contracts.Persistence;

namespace Ordering.Application.Features.Orders.Queries.GetOrderList
{
    public class GetOrderListQueryHandle : IRequestHandler<GetOrderListQuery, List<OrdersDTO>>
    {
        private readonly IOrderRepository _orderRepository;

        public GetOrderListQueryHandle(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public async Task<List<OrdersDTO>> Handle(GetOrderListQuery request, 
             CancellationToken cancellationToken)
        {
            var orderList = await _orderRepository.GetOrdersByUserName(request.UserName);
            return orderList.Adapt<List<OrdersDTO>>();
            
        }
    }
}
