using AutoMapper;
using ShopifyChallenge.Catalog.Domain;
using ShopifyChallenge.Core.DomainObjects;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ShopifyChallenge.Catalog.Application.ViewModels;

namespace ShopifyChallenge.Catalog.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IInventoryService _inventoryService;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository,
                                IInventoryService inventoryService,
                                IMapper mapper)
        {
            _productRepository = productRepository;
            _inventoryService = inventoryService;
            _mapper = mapper;
        }

        public async Task<ProductViewModel> GetById(Guid id)
        {
            return _mapper.Map<ProductViewModel>(await _productRepository.GetById(id));
        }

        public async Task<IEnumerable<ProductViewModel>> GetAll()
        {
            return _mapper.Map<IEnumerable<ProductViewModel>>(await _productRepository.GetAll());
        }

        public async Task Add(ProductViewModel productViewModel)
        {
            var product = _mapper.Map<Product>(productViewModel);
            _productRepository.Add(product);

            await _productRepository.UnitOfWork.Commit();
        }

        public async Task Update(ProductViewModel productViewModel)
        {
            var product = _mapper.Map<Product>(productViewModel);
            _productRepository.Update(product);

            await _productRepository.UnitOfWork.Commit();
        }

        public async Task<ProductViewModel> DecreaseInventory(Guid id, int quantity)
        {
            if (!_inventoryService.DecreaseInventory(id, quantity).Result)
            {
                throw new DomainException("Error, stock not updated");
            }

            return _mapper.Map<ProductViewModel>(await _productRepository.GetById(id));
        }

        public async Task<ProductViewModel> AddInventory(Guid id, int quantity)
        {
            if (!_inventoryService.AddInventory(id, quantity).Result)
            {
                throw new DomainException("Error, stock not replenished");
            }

            return _mapper.Map<ProductViewModel>(await _productRepository.GetById(id));
        }

        public async Task<ProductViewModel> GetImageById(Guid id)
        {
            return _mapper.Map<ProductViewModel>(await _productRepository.GetImageById(id));
        }

        public async Task AddImage(Guid id, ProductImageViewModel productImageViewModel)
        {
            var product = await _productRepository.GetById(id);
            var image = _mapper.Map<ProductImage>(productImageViewModel);
            
            product.AddImage(image);
            _productRepository.AddImage(image);

            await _productRepository.UnitOfWork.Commit();
        }

        public async Task RemoveImage(Guid id)
        {
            _productRepository.RemoveImage(id);

            await _productRepository.UnitOfWork.Commit();
        }

        public void Dispose()
        {
            _productRepository?.Dispose();
        }
    }
}
