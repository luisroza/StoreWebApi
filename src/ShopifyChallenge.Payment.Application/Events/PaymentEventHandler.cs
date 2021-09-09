﻿using MediatR;
using ShopifyChallenge.Core.DomainObjects.DTO;
using ShopifyChallenge.Payment.Domain;
using System.Threading;
using System.Threading.Tasks;
using ShopifyChallenge.Core.Communication.Messages.IntegrationEvents;

namespace ShopifyChallenge.Payment.Application.Events
{
    public class PaymentEventHandler : INotificationHandler<OrderInventoryConfirmedEvent>
    {
        private readonly IPaymentService _paymentService;

        public PaymentEventHandler(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        public async Task Handle(OrderInventoryConfirmedEvent message, CancellationToken cancellationToken)
        {
            var orderPayment = new OrderPayment
            {
                OrderId = message.OrderId,
                CustomerId = message.CustomerId,
                Total = message.Total,
                CardName = message.CardName,
                CardNumber = message.CardNumber,
                CardExpirationDate = message.CardExpirationDate,
                CardVerificationCode = message.CardVerificationCode
            };

            await _paymentService.CheckOut(orderPayment);
        }
    }
}