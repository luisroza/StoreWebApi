using AutoMapper;
using Store.Catalog.Application.ViewModels;
using Store.Catalog.Domain;

namespace Store.Catalog.Application.AutoMapper
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
