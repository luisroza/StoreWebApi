using AutoMapper;
using StoreApi.Catalog.Application.ViewModels;
using StoreApi.Catalog.Domain;

namespace StoreApi.Catalog.Application.AutoMapper
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
