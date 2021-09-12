using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ShopifyChallenge.Catalog.Application.ViewModels;

namespace ShopifyChallenge.Catalog.Application.Services
{
    public interface IProductService : IDisposable
    {
        Task<ProductViewModel> GetById(Guid id);
        Task<IEnumerable<ProductViewModel>> GetAll();

        Task AddProduct(ProductViewModel productViewModel);
        Task UpdateProduct(ProductViewModel productViewModel);

        Task<ProductViewModel> DecreaseInventory(Guid id, int quantity);
        Task<ProductViewModel> AddInventory(Guid id, int quantity);
    }
}
