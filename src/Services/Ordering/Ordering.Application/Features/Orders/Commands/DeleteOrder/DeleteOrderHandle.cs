using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Contracts.Persistence;
using Ordering.Domain.Common;

namespace Ordering.Application.Features.Orders.Commands.DeleteOrder
{
    public class DeleteOrderHandle : IRequestHandler<DeleteOrderCommand, OperationResult>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ILogger<DeleteOrderHandle> _logger;

        public DeleteOrderHandle(IOrderRepository orderRepository,ILogger<DeleteOrderHandle> logger)
        {
            _orderRepository = orderRepository;
            _logger = logger;
        }


        /// <inheritdoc />
        public async Task<OperationResult> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            var orderToDelete = await _orderRepository.GetByIdAsync(request.Id);
            if (orderToDelete == null)
            {
                _logger.LogError($"Order:{request.Id} not exist on database");
                // exception is not free, we return OperationResult rather than a exception
                return OperationResult.Error($"Order:{request.Id} not exist on database");
            }
            await _orderRepository.DeleteAsync(orderToDelete);
            _logger.LogInformation($"Order:{request.Id} is successful deleted");
            return OperationResult.Success();
        }
    }
}
