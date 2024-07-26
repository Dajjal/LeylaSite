using Core.Application.Generics;
using Core.Application.Models.Instagram;
using Core.Domain.Models.Instagram;
using WebAPI.Controllers.Generics;

namespace WebAPI.Controllers.Instagram;

public class InstagramPostsController(IGenericService<InstagramPostModel> service)
    : GenericController<InstagramPostModel, InstagramPostDto>(service);