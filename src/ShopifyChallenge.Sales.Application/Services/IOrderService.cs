using ShopifyChallenge.Sales.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShopifyChallenge.Sales.Application.Services
{
    public interface IOrderService
    {
        Task<CartViewModel> GetCustomerCart(Guid customerId);
        Task<IEnumerable<OrderViewModel>> GetCustomerOrders(Guid customerId);
    }
}
