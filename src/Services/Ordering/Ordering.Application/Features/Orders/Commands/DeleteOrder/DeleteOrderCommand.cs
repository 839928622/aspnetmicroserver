using MediatR;
using Ordering.Domain.Common;

namespace Ordering.Application.Features.Orders.Commands.DeleteOrder
{
  public  class DeleteOrderCommand : IRequest<OperationResult>
    {
        public int Id { get; set; }
    }
}
