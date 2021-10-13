using Store.Sales.Application.ViewModels;
using Store.Sales.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Store.Sales.Application.Services
{
    public interface IOrderService
    {
        Task<CartViewModel> GetCustomerCart(Guid customerId);
        Task<IEnumerable<OrderViewModel>> GetCustomerOrders(Guid customerId);
        Task<Order> GetById(Guid orderId);
    }
}
