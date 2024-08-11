using AutoMapper;
using Core.Application.Models.Store.Admin;
using Core.Domain.Models.Store.Admin;

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
    }
}