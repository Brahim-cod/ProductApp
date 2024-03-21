using AutoMapper;
using ProductApp.ViewModel;
using Shared.Models;

namespace ProductApp.Configuration;

public class AutomapperProfiles : Profile
{
    public AutomapperProfiles()
    {
        CreateMap<CreateProduct, Product>()
            .ForMember(
                dest => dest.Name,
                src => src.MapFrom(x => x.ProductName)
            )
            .ForMember(
                dest => dest.Description,
                src => src.MapFrom(x => x.ProductDescription)
            )
            .ForMember(
                dest => dest.Image,
                src => src.MapFrom(x => x.ProductImage)
            )
            .ForMember(
                dest => dest.Price,
                src => src.MapFrom(x => x.ProductPrice)
            )
            .ForMember(
                dest => dest.Quantity,
                src => src.MapFrom(x => x.ProductQuantity)
            )
            .ForMember(
                dest => dest.CategoryId,
                src => src.MapFrom(x => x.ProductCategoryID)
            )
            .ReverseMap();


        CreateMap<Product, ListProduct>()
           .ForMember(
               dest => dest.ProductID,
               src => src.MapFrom(x => x.Id)
           )
           .ForMember(
               dest => dest.ProductName,
               src => src.MapFrom(x => x.Name)
           )
           .ForMember(
               dest => dest.ProductDescription,
               src => src.MapFrom(x => x.Description)
           )
           .ForMember(
               dest => dest.ProductImage,
               src => src.MapFrom(x => x.Image)
           )
           .ForMember(
               dest => dest.ProductPrice,
               src => src.MapFrom(x => x.Price)
           )
           .ForMember(
               dest => dest.ProductQuantity,
               src => src.MapFrom(x => x.Quantity)
           )
           .ForMember(
               dest => dest.ProductCategoryName,
               src => src.MapFrom(x => x.Category.Name)
           );
    }
}
