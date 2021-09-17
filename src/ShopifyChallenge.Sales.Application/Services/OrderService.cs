using ShopifyChallenge.Sales.Application.ViewModels;
using ShopifyChallenge.Sales.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopifyChallenge.Sales.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<CartViewModel> GetCustomerCart(Guid customerId)
        {
            var order = await _orderRepository.GetDraftOrderByCustomerId(customerId);
            if (order == null) return null;

            var cart = new CartViewModel
            {
                CustomerId = order.CustomerId,
                TotalPrice = order.TotalPrice,
                OrderId = order.Id,
                DiscountPrice = order.Discount,
                SubTotal = order.TotalPrice + order.Discount,
                CouponCode = order.CouponId.HasValue ? order.Coupon.Code : null
            };

            foreach (var line in order.OrderLines)
            {
                cart.Lines.Add(new CartOrderLineViewModel
                {
                    ProductId = line.ProductId,
                    ProductName = line.ProductName,
                    Quantity = line.Quantity,
                    UnitPrice = line.UnitPrice,
                    TotalPrice = line.UnitPrice * line.Quantity
                });
            }

            return cart;
        }

        public async Task<IEnumerable<OrderViewModel>> GetCustomerOrders(Guid customerId)
        {
            var orders = await _orderRepository.GetListByCustomerId(customerId);

            // TODO: create a list in a config file
            orders = orders.Where(o => o.OrderStatus == OrderStatus.Paid || o.OrderStatus == OrderStatus.Cancelled)
                .OrderByDescending(o => o.Code);

            if (!orders.Any()) return null;

            var ordersView = new List<OrderViewModel>();

            foreach (var order in orders)
            {
                ordersView.Add(new OrderViewModel
                {
                    Code = order.Code,
                    CreateDate = order.CreateDate,
                    OrderStatus = (int)order.OrderStatus,
                    TotalPrice = order.TotalPrice
                });
            }

            return ordersView;
        }

        public async Task<Order> GetById(Guid orderId)
        {
            return await _orderRepository.GetById(orderId);
        }
    }
}
