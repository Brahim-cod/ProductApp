using AutoMapper;
using Repository.Models;
using Services.ModelsDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Mapper;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        #region ProductMapping
        
        CreateMap<CreateProductDto, Product>()
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


        CreateMap<Product, ProductDto>()
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

        CreateMap<UpdateProductDto, Product>()
            .ForMember(
                dest => dest.Id,
                src => src.MapFrom(x => x.ProductID)
            )
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
        #endregion

        #region CategoryMapping
        CreateMap<Category, CategoryDto>()
            .ForMember(
                dest => dest.Id,
                src => src.MapFrom(x => x.Id)
            )
            .ForMember(
                dest => dest.Name,
                src => src.MapFrom(x => x.Name)
            )
            .ReverseMap();
        #endregion

    }
}
