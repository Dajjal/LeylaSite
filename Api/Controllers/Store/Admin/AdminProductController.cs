using Api.Controllers.Generics;
using Core.Application.Generics;
using Core.Application.Models.Store.Admin;
using Core.Domain.Models.Store.Admin;

namespace Api.Controllers.Store.Admin;

public class AdminProductController(IGenericService<ProductModel> service)
    : GenericController<ProductModel, ProductDto>(service);