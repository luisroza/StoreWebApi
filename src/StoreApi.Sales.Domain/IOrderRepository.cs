﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using StoreApi.Core.Data;

namespace StoreApi.Sales.Domain
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<Order> GetById(Guid id);
        Task<IEnumerable<Order>> GetListByCustomerId(Guid customerId);
        Task<Order> GetDraftOrderByCustomerId(Guid customerId);
        void Add(Order order);
        void Update(Order order);

        Task<OrderLine> GetOrderLineById(Guid id);
        Task<OrderLine> GetOrderLineByOrder(Guid orderId, Guid productId);
        void AddOrderLine(OrderLine orderLine);
        void UpdateOrderLine(OrderLine orderLine);
        void RemoveOrderLine(OrderLine orderLine);

        Task<Coupon> GetCouponByCode(string code);
    }
}
