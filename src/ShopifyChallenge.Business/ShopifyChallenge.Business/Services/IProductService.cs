using Store.Catalog.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Store.Catalog.Application.Services
{
    public interface IProductService : IDisposable
    {
        Task<ProductViewModel> GetById(Guid id);
        Task<IEnumerable<ProductViewModel>> GetAll();

        Task Add(ProductViewModel productViewModel);
        Task Update(ProductViewModel productViewModel);

        Task<ProductViewModel> DecreaseInventory(Guid id, int quantity);
        Task<ProductViewModel> AddInventory(Guid id, int quantity);

        Task<ProductViewModel> GetImageById(Guid id);
        Task AddImage(Guid id, ProductImageViewModel productImageView);
        Task RemoveImage(Guid id);
    }
}
