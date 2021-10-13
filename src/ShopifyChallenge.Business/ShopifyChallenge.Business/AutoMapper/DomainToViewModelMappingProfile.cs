using AutoMapper;
using Store.Catalog.Application.ViewModels;
using Store.Catalog.Domain;

namespace Store.Catalog.Application.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<Product, ProductViewModel>();

            CreateMap<ProductImage, ProductImageViewModel>();
        }
    }
}
