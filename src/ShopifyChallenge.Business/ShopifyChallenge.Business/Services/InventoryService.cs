using ShopifyChallenge.Catalog.Application.Events;
using ShopifyChallenge.Catalog.Data.Repository;
using ShopifyChallenge.Core.Communication.Mediator;
using ShopifyChallenge.Core.Communication.Messages.Notifications;
using ShopifyChallenge.Core.DomainObjects.DTO;
using System;
using System.Threading.Tasks;
using ShopifyChallenge.Catalog.Domain;

namespace ShopifyChallenge.Catalog.Application.Services
{
    public class InventoryService : IInventoryService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMediatorHandler _mediatorHandler;

        public InventoryService(IProductRepository productRepository,
            IMediatorHandler mediatorHandler)
        {
            _productRepository = productRepository;
            _mediatorHandler = mediatorHandler;
        }

        public async Task<bool> DecreaseInventory(Guid productId, int quantity)
        {
            if (!await DecreaseInventoryItem(productId, quantity)) return false;
            return await _productRepository.UnitOfWork.Commit();
        }

        public async Task<bool> DecreaseInventoryProductList(OrderItemList list)
        {
            foreach (var item in list.Lines)
            {
                if (!await DecreaseInventoryItem(item.Id, item.Quantity)) return false;
            }

            return await _productRepository.UnitOfWork.Commit();
        }

        private async Task<bool> DecreaseInventoryItem(Guid productId, int quantity)
        {
            var product = await _productRepository.GetById(productId);
            if (product == null) return false;

            if (!product.HasInventory(quantity))
            {
                await _mediatorHandler.PublishNotification(new DomainNotification("Inventory",
                    $"Order - {product.Name} has no stock"));
                return false;
            }

            product.DecreaseInventory(quantity);

            //Business Rule -> place it into a config file
            if (product.StockQuantity < 10)
            {
                await _mediatorHandler.PublishDomainEvent(new InventoryEvent(product.Id, product.StockQuantity));
            }

            _productRepository.Update(product);
            return true;
        }

        public async Task<bool> ReplenishInventory(Guid productId, int quantity)
        {
            var product = await _productRepository.GetById(productId);

            if (product == null) return false;
            product.ReplenishInventory(quantity);

            _productRepository.Update(product);
            return await _productRepository.UnitOfWork.Commit();
        }

        public async Task<bool> ReplenishInventoryOrderProductsList(OrderItemList list)
        {
            foreach (var item in list.Lines)
            {
                await ReplenishInventory(item.Id, item.Quantity);
            }

            return await _productRepository.UnitOfWork.Commit();
        }

        public async Task<bool> ReplenishInventoryItem(Guid productId, int quantity)
        {
            var product = await _productRepository.GetById(productId);

            if (product == null) return false;
            product.ReplenishInventory(quantity);

            _productRepository.Update(product);
            return true;
        }

        public void Dispose()
        {
            _productRepository?.Dispose();
        }
    }
}
