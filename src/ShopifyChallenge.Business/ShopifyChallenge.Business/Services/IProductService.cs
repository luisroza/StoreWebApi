using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ShopifyChallenge.Catalog.Application.ViewModel;

namespace ShopifyChallenge.Catalog.Application.Services
{
    public interface IProductService : IDisposable
    {
        Task<IEnumerable<ProductViewModel>> GetByCategory(int code);
        Task<ProductViewModel> GetById(Guid id);
        Task<IEnumerable<ProductViewModel>> GetAll();
        Task<IEnumerable<CategoryViewModel>> GetCategories();

        Task AddProduct(ProductViewModel productViewModel);
        Task UpdateProduct(ProductViewModel productViewModel);

        Task<ProductViewModel> DecreaseStock(Guid id, int quantity);
        Task<ProductViewModel> ReplenishStock(Guid id, int quantity);
    }
}
