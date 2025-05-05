using AutoMapper;
using SneakerShop.Domain.Entities;
using SneakerShop.SharedViewModel.Responses.Category;
using SneakerShop.SharedViewModel.Responses.Product;
using SneakerShop.SharedViewModel.Responses.Review;


namespace SneakerShop.Application.Common.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductResponse>()
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src =>
                    src.ProductImages != null && src.ProductImages.Any() ? src.ProductImages.First().ImageUrl : null));
            CreateMap<Product, ProductDetailResponse>()
                .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.ProductImages.Select(pi => pi.ImageUrl).ToList()))
                .ForMember(dest => dest.ProductVariants, opt => opt.MapFrom(src => src.ProductVariants));
            CreateMap<ProductVariant, ProductVariantResponse>();
            CreateMap<Review, ReviewResponse>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.Name));

            CreateMap<Category, CategoryResponse>();
        }
    }
}