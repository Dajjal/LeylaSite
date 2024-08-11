using AutoMapper;
using Core.Application.Models.Store.Admin;
using Core.Domain.Models.Store.Site;

namespace Core.Application.Mappings;

/// <summary>
///     Мапинги
/// </summary>
public class LeylaSiteProfileMapping : Profile
{
    public LeylaSiteProfileMapping()
    {
        // Товары
        CreateMap<ProductModel, ProductDto>();
        CreateMap<ProductDto, ProductModel>();

        // Категории товаров
        CreateMap<CategoryModel, CategoryDto>();
        CreateMap<CategoryDto, CategoryModel>();
    }
}