using ShopifyChallenge.Sales.Application.ViewModels;
using ShopifyChallenge.Sales.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShopifyChallenge.Sales.Application.Services
{
    public interface IOrderService
    {
        Task<CartViewModel> GetCustomerCart(Guid customerId);
        Task<IEnumerable<OrderViewModel>> GetCustomerOrders(Guid customerId);
        Task<Order> GetById(Guid orderId);
    }
}
