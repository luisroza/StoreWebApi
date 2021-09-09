using System;
using System.Threading.Tasks;
using ShopifyChallenge.Core.DomainObjects.DTO;

namespace ShopifyChallenge.Catalog.Application.Services
{
    public interface IInventoryService : IDisposable
    {
        Task<bool> DecreaseInventory(Guid productId, int quantity);
        Task<bool> DecreaseInventoryProductList(OrderItemList list);
        Task<bool> ReplenishInventory(Guid productId, int quantity);
        Task<bool> ReplenishInventoryOrderProductsList(OrderItemList list);
    }
}
