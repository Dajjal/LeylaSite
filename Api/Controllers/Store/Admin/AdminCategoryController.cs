using Api.Controllers.Generics;
using Core.Application.Generics;
using Core.Application.Models.Store.Admin;
using Core.Domain.Models.Store.Site;

namespace Api.Controllers.Store.Admin;

public class AdminCategoryController(IGenericService<CategoryModel> service)
    : GenericController<CategoryModel, CategoryDto>(service);