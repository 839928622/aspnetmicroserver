using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Ordering.Application.Features.Orders.Queries.GetOrderList
{
    public class GetOrderListQueryHandle : IRequestHandler<GetOrderListQuery, List<OrdersDTO>>
    {
        public GetOrderListQueryHandle()
        {
            
        }
        public Task<List<OrdersDTO>> Handle(GetOrderListQuery request, 
             CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
