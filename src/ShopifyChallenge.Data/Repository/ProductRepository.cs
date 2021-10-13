using Microsoft.EntityFrameworkCore;
using Store.Catalog.Domain;
using Store.Core.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Store.Catalog.Data.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly CatalogContext _context;

        public ProductRepository(CatalogContext context)
        {
            _context = context;
        }

        //UnitOfWork reflects _context
        public IUnitOfWork UnitOfWork => _context;

        public async Task<IEnumerable<Product>> GetAll()
        {
            //AsNoTracking uses less resources from EF
            return await _context.Products.AsNoTracking().ToListAsync();
        }

        public async Task<Product> GetById(Guid id)
        {
            return await _context.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
        }

        public void Add(Product product)
        {
            _context.Products.Add(product);
        }

        public void Update(Product product)
        {
            _context.Products.Update(product);
        }

        public async Task<ProductImage> GetImageById(Guid id)
        {
            return await _context.ProductImages.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
        }

        public void AddImage(ProductImage productImage)
        {
            _context.ProductImages.Add(productImage);
        }

        public async void RemoveImage(Guid id)
        {
            var productImage = await _context.ProductImages.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
            _context.ProductImages.Remove(productImage);
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
