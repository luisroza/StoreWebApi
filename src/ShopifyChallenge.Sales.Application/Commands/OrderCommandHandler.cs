﻿using MediatR;
using ShopifyChallenge.Core.Communication.Mediator;
using ShopifyChallenge.Core.Communication.Messages;
using ShopifyChallenge.Core.Communication.Messages.IntegrationEvents;
using ShopifyChallenge.Core.Communication.Messages.Notifications;
using ShopifyChallenge.Core.DomainObjects.DTO;
using ShopifyChallenge.Core.Extentions;
using ShopifyChallenge.Sales.Application.Events;
using ShopifyChallenge.Sales.Domain;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ShopifyChallenge.Sales.Application.Commands
{
    public class OrderCommandHandler : IRequestHandler<AddOrderLineCommand, bool>,
        IRequestHandler<ApplyCouponCommand, bool>,
        IRequestHandler<RemoveOrderLineCommand, bool>,
        IRequestHandler<UpdateOrderLineCommand, bool>,
        IRequestHandler<StartOrderCommand, bool>,
        IRequestHandler<FinalizeOrderCommand, bool>,
        IRequestHandler<CancelOrderReplenishInventoryCommand, bool>,
        IRequestHandler<CancelOrderCommand, bool>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMediatorHandler _mediatorHandler;

        public OrderCommandHandler(IOrderRepository orderRepository, IMediatorHandler mediatorHandler)
        {
            _orderRepository = orderRepository;
            _mediatorHandler = mediatorHandler;
        }

        public async Task<bool> Handle(AddOrderLineCommand message, CancellationToken cancellationToken)
        {
            if (!ValidateCommand(message)) return false;

            var order = await _orderRepository.GetDraftOrderByCustomerId(message.CustomerId);
            var orderLine = new OrderLine(message.ProductId, message.Name, message.Quantity, message.UnitPrice);

            if (order == null)
            {
                order = Order.OrderFactory.NewDraftOrder(message.CustomerId);
                order.AddOrderLine(orderLine);

                _orderRepository.Add(order);
                order.AddEvent(new DraftOrderStartedEvent(order.CustomerId, order.Id));
            }
            else
            {
                var existentOrderLine = order.ExistingOrderLine(orderLine);
                order.AddOrderLine(orderLine);

                if (existentOrderLine)
                {
                    _orderRepository.UpdateOrderLine(order.OrderLines.FirstOrDefault(p => p.ProductId == orderLine.ProductId));
                }
                else
                {
                    _orderRepository.AddOrderLine(orderLine);
                }

                order.AddEvent(new UpdateOrderEvent(order.CustomerId, order.Id, order.TotalPrice));
            }

            order.AddEvent(new AddOrderLineEvent(order.CustomerId, order.Id, message.ProductId, message.Name, message.UnitPrice, message.Quantity));
            return await _orderRepository.UnitOfWork.Commit();
        }

        public async Task<bool> Handle(ApplyCouponCommand message, CancellationToken cancellationToken)
        {
            if (!ValidateCommand(message)) return false;

            var order = await _orderRepository.GetDraftOrderByCustomerId(message.CustomerId);
            if (order == null)
            {
                //Customer will receive all notifications
                await _mediatorHandler.PublishNotification(new DomainNotification("order", "Order not found"));
                return false;
            }

            var coupon = await _orderRepository.GetCouponByCode(message.CouponCode);
            if (coupon == null)
            {
                await _mediatorHandler.PublishNotification(new DomainNotification("order", "Coupon not found"));
                return false;
            }

            var couponValidation = order.ApplyCoupon(coupon);
            if (!couponValidation.IsValid)
            {
                foreach (var error in couponValidation.Errors)
                {
                    await _mediatorHandler.PublishNotification(new DomainNotification(error.ErrorCode, error.ErrorMessage));
                }
                return false;
            }

            order.AddEvent(new UpdateOrderEvent(order.CustomerId, order.Id, order.TotalPrice));
            order.AddEvent(new CouponAppliedOrderEvent(message.CustomerId, order.Id, coupon.Id));

            _orderRepository.Update(order);
            // TODO: update voucher on DB

            return await _orderRepository.UnitOfWork.Commit();
        }

        public async Task<bool> Handle(RemoveOrderLineCommand message, CancellationToken cancellationToken)
        {
            if (!ValidateCommand(message)) return false;

            var order = await _orderRepository.GetDraftOrderByCustomerId(message.CustomerId);
            if (order == null)
            {
                //Customer will receive all notifications
                await _mediatorHandler.PublishNotification(new DomainNotification("order", "Order not found"));
                return false;
            }

            var orderLine = await _orderRepository.GetOrderLineByOrder(order.Id, message.ProductId);
            if (order != null && !order.ExistingOrderLine(orderLine))
            {
                await _mediatorHandler.PublishNotification(new DomainNotification("order", "Order Line not found"));
                return false;
            }

            order.RemoveOrderLine(orderLine);
            order.AddEvent(new UpdateOrderEvent(order.CustomerId, order.Id, order.TotalPrice));
            order.AddEvent(new RemoveOrderLineEvent(message.CustomerId, order.Id, message.ProductId));

            _orderRepository.RemoveOrderLine(orderLine);
            _orderRepository.Update(order);

            return await _orderRepository.UnitOfWork.Commit();
        }

        public async Task<bool> Handle(UpdateOrderLineCommand message, CancellationToken cancellationToken)
        {
            if (!ValidateCommand(message)) return false;

            var order = await _orderRepository.GetDraftOrderByCustomerId(message.CustomerId);
            if (order == null)
            {
                //Customer will receive all notifications
                await _mediatorHandler.PublishNotification(new DomainNotification("order", "Order not found"));
                return false;
            }

            var orderLine = await _orderRepository.GetOrderLineByOrder(order.Id, message.ProductId);
            if (!order.ExistingOrderLine(orderLine))
            {
                await _mediatorHandler.PublishNotification(new DomainNotification("order", "Order Line not found"));
                return false;
            }

            order.UpdateUnits(orderLine, message.Quantity);

            order.AddEvent(new UpdateOrderEvent(order.CustomerId, order.Id, order.TotalPrice));
            order.AddEvent(new UpdateProductOrderEvent(order.CustomerId, order.Id, message.ProductId, message.Quantity));

            _orderRepository.UpdateOrderLine(orderLine);
            _orderRepository.Update(order);

            return await _orderRepository.UnitOfWork.Commit();
        }

        public async Task<bool> Handle(StartOrderCommand message, CancellationToken cancellationToken)
        {
            if (!ValidateCommand(message)) return false;

            var order = await _orderRepository.GetDraftOrderByCustomerId(message.CustomerId);
            order.StarOrder();

            var itemList = new List<Item>();
            order.OrderLines.ForEach(i => itemList.Add(new Item { Id = i.ProductId, Quantity = i.Quantity }));
            var orderItemList = new OrderItemList { OrderId = order.Id, Lines = itemList };

            order.AddEvent(new StartOrderEvent(order.Id, order.CustomerId, orderItemList, order.TotalPrice, message.CardName, message.CardNumber, message.CardExpirationDate, message.CardVerificationCode));

            _orderRepository.Update(order);
            return await _orderRepository.UnitOfWork.Commit();
        }

        public async Task<bool> Handle(FinalizeOrderCommand message, CancellationToken cancellationToken)
        {
            if (!ValidateCommand(message)) return false;
            var order = await _orderRepository.GetById(message.OrderId);

            if (order == null)
            {
                await _mediatorHandler.PublishNotification(new DomainNotification("order", "Order not found"));
                return false;
            }

            order.FinalizeOrder();

            order.AddEvent(new OrderFinalizedEvent(message.OrderId));
            return await _orderRepository.UnitOfWork.Commit();
        }

        public async Task<bool> Handle(CancelOrderReplenishInventoryCommand message, CancellationToken cancellationToken)
        {
            if (!ValidateCommand(message)) return false;
            var order = await _orderRepository.GetById(message.OrderId);

            if (order == null)
            {
                await _mediatorHandler.PublishNotification(new DomainNotification("order", "Order not found"));
                return false;
            }

            var itemsList = new List<Item>();
            order.OrderLines.ForEach(i => itemsList.Add(new Item { Id = i.ProductId, Quantity = i.Quantity }));
            var orderItemList = new OrderItemList { OrderId = order.Id, Lines = itemsList };

            order.AddEvent(new OrderCancelledEvent(order.Id, order.CustomerId, orderItemList));
            order.MakeDraft();

            return await _orderRepository.UnitOfWork.Commit();
        }

        public async Task<bool> Handle(CancelOrderCommand message, CancellationToken cancellationToken)
        {
            if (!ValidateCommand(message)) return false;
            var order = await _orderRepository.GetById(message.OrderId);

            if (order == null)
            {
                await _mediatorHandler.PublishNotification(new DomainNotification("order", "Order not found"));
                return false;
            }

            order.CancelOrder();
            return await _orderRepository.UnitOfWork.Commit();
        }

        private bool ValidateCommand(Command message)
        {
            if (message.IsValid()) return true;

            foreach (var error in message.ValidationResult.Errors)
            {
                _mediatorHandler.PublishNotification(new DomainNotification(message.MessageType, error.ErrorMessage));
            }

            return false;
        }
    }
}
