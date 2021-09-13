using ShopifyChallenge.Core.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShopifyChallenge.Catalog.Domain
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<IEnumerable<Product>> GetAll();
        Task<Product> GetById(Guid id);

        void Add(Product product);
        void Update(Product product);

        Task<ProductImage> GetImageById(Guid id);
        void AddImage(ProductImage productImage);
        void RemoveImage(Guid id);
    }
}
