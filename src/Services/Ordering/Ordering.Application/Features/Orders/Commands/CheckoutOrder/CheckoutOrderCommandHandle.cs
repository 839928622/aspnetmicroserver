﻿using System;
using System.Threading;
using System.Threading.Tasks;
using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Contracts.Infrastructure;
using Ordering.Application.Contracts.Persistence;
using Ordering.Application.Models;
using Ordering.Domain.Entities;

namespace Ordering.Application.Features.Orders.Commands.CheckoutOrder
{
    public  class CheckoutOrderCommandHandle: IRequestHandler<CheckoutOrderCommand,int>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IEmailService _emailService;
        private readonly ILogger<CheckoutOrderCommandHandle> _logger;

        public CheckoutOrderCommandHandle(IOrderRepository orderRepository, IEmailService emailService, ILogger<CheckoutOrderCommandHandle> logger)
        {
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
            _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }


        /// <inheritdoc />
        public async Task<int> Handle(CheckoutOrderCommand request, CancellationToken cancellationToken)
        {
            var orderEntity = request.Adapt<Order>();
            var newOrder =  await _orderRepository.AddAsync(orderEntity);
            _logger.LogInformation($"Order {newOrder.Id} is successfully created.");
            // this should send a message to MQ,let MQ do asynchronous message
            await SendMail(newOrder);
            return newOrder.Id;
        }

        private async Task SendMail(Order order)
        {
            var email = new Email() { To = "myemail@gmail.com", Body = $"Order was created.", Subject = "Order was created" };

            try
            {
                await _emailService.SendEmail(email);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Order {order.Id} failed due to an error with the mail service: {ex.Message}");
            }
        }
    }
}
