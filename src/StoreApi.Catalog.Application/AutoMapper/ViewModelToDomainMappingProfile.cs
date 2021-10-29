using AutoMapper;
using StoreApi.Catalog.Application.ViewModels;
using StoreApi.Catalog.Domain;

namespace StoreApi.Catalog.Application.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<ProductViewModel, Product>()
                .ConstructUsing(p => new Product(p.Name, p.Description, p.Active,
                        p.Price, p.CreateDate));

            CreateMap<ProductImageViewModel, ProductImage>()
                .ConstructUsing(c => new ProductImage(c.ProductId, c.Name, c.CreateDate));
        }
    }
}
