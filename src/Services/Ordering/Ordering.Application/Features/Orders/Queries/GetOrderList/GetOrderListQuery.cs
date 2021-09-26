using System;
using System.Collections.Generic;
using MediatR;

namespace Ordering.Application.Features.Orders.Queries.GetOrderList
{
    public  class GetOrderListQuery :  IRequest<List<OrdersDTO>>
    {
        public GetOrderListQuery(string userName)
        {
            this.UserName = userName ?? throw new  ArgumentNullException(nameof(userName));
        }
        public string UserName { get; set; }

    }
}
