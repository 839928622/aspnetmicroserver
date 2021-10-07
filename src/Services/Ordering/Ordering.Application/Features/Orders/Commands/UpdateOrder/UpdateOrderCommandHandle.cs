using System;
using System.Threading;
using System.Threading.Tasks;
using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Contracts.Persistence;

namespace Ordering.Application.Features.Orders.Commands.UpdateOrder
{
    public class UpdateOrderCommandHandle : IRequestHandler<UpdateOrderCommand>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly Logger<UpdateOrderCommandHandle> _logger;

        public UpdateOrderCommandHandle(IOrderRepository orderRepository,Logger<UpdateOrderCommandHandle> logger)
        {
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        /// <inheritdoc />
        public async Task<Unit> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            var orderToUpdate = await _orderRepository.GetByIdAsync(request.Id);
            if (orderToUpdate == null)
            {
                _logger.LogError($"Order ({request.Id}) you are updating is currently not exist on database");
                throw new NullReferenceException($"Order ({request.Id}) you are updating is currently not exist on database");
            }

            request.Adapt(orderToUpdate);
            await _orderRepository.UpdateAsync(orderToUpdate);

            _logger.LogInformation($"Order {orderToUpdate.Id}");

            return Unit.Value;
        }
    }
}
