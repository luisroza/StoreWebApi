using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using ShopifyChallenge.Catalog.Application.ViewModel;
using ShopifyChallenge.Catalog.Data.Repository;
using ShopifyChallenge.Catalog.Domain;
using ShopifyChallenge.Core.DomainObjects;

namespace ShopifyChallenge.Catalog.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IInventoryService _inventoryService;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository,
                                 IMapper mapper,
                                 IInventoryService stockService)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _inventoryService = stockService;
        }

        public async Task<IEnumerable<ProductViewModel>> GetByCategory(int code)
        {
            return _mapper.Map<IEnumerable<ProductViewModel>>(await _productRepository.GetByCategory(code));
        }

        public async Task<ProductViewModel> GetById(Guid id)
        {
            return _mapper.Map<ProductViewModel>(await _productRepository.GetById(id));
        }

        public async Task<IEnumerable<ProductViewModel>> GetAll()
        {
            return _mapper.Map<IEnumerable<ProductViewModel>>(await _productRepository.GetAll());
        }

        public async Task<IEnumerable<CategoryViewModel>> GetCategories()
        {
            return _mapper.Map<IEnumerable<CategoryViewModel>>(await _productRepository.GetCategories());
        }

        public async Task AddProduct(ProductViewModel productViewModel)
        {
            var product = _mapper.Map<Product>(productViewModel);
            _productRepository.Add(product);

            await _productRepository.UnitOfWork.Commit();
        }

        public async Task UpdateProduct(ProductViewModel productViewModel)
        {
            var product = _mapper.Map<Product>(productViewModel);
            _productRepository.Update(product);

            await _productRepository.UnitOfWork.Commit();
        }

        public async Task<ProductViewModel> DecreaseStock(Guid id, int quantity)
        {
            if (!_inventoryService.DecreaseInventory(id, quantity).Result)
            {
                throw new DomainException("Error, stock not updated");
            }

            return _mapper.Map<ProductViewModel>(await _productRepository.GetById(id));
        }

        public async Task<ProductViewModel> ReplenishStock(Guid id, int quantity)
        {
            if (!_inventoryService.ReplenishInventory(id, quantity).Result)
            {
                throw new DomainException("Error, stock not replenished");
            }

            return _mapper.Map<ProductViewModel>(await _productRepository.GetById(id));
        }

        public void Dispose()
        {
            _productRepository?.Dispose();
        }
    }
}
