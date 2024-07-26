using AutoMapper;
using Core.Application.Models.Instagram;
using Core.Domain.Models.Instagram;

namespace Core.Application.Mappings;

/// <summary>
/// Мапинги для сайта
/// </summary>
public class SiteProfileMapping: Profile
{
    public SiteProfileMapping()
    {
        #region Instagram
        
        // Посты
        CreateMap<InstagramPostModel, InstagramPostDto>();
        CreateMap<InstagramPostDto, InstagramPostModel>();
        
        #endregion
    }
}