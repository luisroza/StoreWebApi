using AutoMapper;
using ShopifyChallenge.Catalog.Application.ViewModels;
using ShopifyChallenge.Catalog.Domain;

namespace ShopifyChallenge.Catalog.Application.AutoMapper
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
