using AutoMapper;
using StoreApp.Core.Models;
using StoreApp.DatabaseProvider.Models;

namespace StoreApp
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<ProductClient, Product>();

            CreateMap<Product, ProductClient>();

            CreateMap<OrderClient, Order>();

            CreateMap<OrderClient, Customer>()
           .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.UserEmail))
           .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName));

            CreateMap<OrderDetailClient, ProductOrder>()
           .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
           .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
           .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity));
        }
    }
}
