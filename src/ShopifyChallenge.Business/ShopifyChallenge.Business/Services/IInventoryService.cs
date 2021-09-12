using System;
using System.Threading.Tasks;
using ShopifyChallenge.Core.DomainObjects.DTO;

namespace ShopifyChallenge.Catalog.Application.Services
{
    public interface IInventoryService : IDisposable
    {
        Task<bool> DecreaseInventory(Guid productId, int quantity);
        Task<bool> DecreaseInventoryProductList(OrderItemList list);
        Task<bool> AddInventory(Guid productId, int quantity);
        Task<bool> AddInventoryOrderProductsList(OrderItemList list);
    }
}
