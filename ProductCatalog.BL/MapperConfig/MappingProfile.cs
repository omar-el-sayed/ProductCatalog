using AutoMapper;
using ProductCatalog.BL.DTOs;
using ProductCatalog.DAL.Models;

namespace ProductCatalog.BL.MapperConfig
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<BaseObject, BaseObjectDto>();
            CreateMap<BaseObjectDto, BaseObject>();

            CreateMap<Product, ProductDto>();
            CreateMap<ProductDto, Product>();

            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryDto, Category>();
        }
    }
}
