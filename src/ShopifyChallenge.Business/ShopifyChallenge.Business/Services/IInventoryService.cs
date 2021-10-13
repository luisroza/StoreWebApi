using System;
using System.Threading.Tasks;
using Store.Core.DomainObjects.DTO;

namespace Store.Catalog.Application.Services
{
    public interface IInventoryService : IDisposable
    {
        Task<bool> DecreaseInventory(Guid productId, int quantity);
        Task<bool> DecreaseInventoryProductList(OrderItemList list);
        Task<bool> AddInventory(Guid productId, int quantity);
        Task<bool> AddInventoryOrderProductsList(OrderItemList list);
    }
}
