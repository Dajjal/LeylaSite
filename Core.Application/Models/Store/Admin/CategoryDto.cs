using Core.Domain.Abstracts;

namespace Core.Application.Models.Store.Admin;

/// <summary>
///     Представляет категорию в модели передачи данных (DTO).
/// </summary>
public class CategoryDto(string name) : AbstractGuidModel
{
    /// <summary>
    ///     Наименование категории.
    /// </summary>
    public string Name { get; init; } = name;

    /// <summary>
    ///     Список товаров, принадлежащих данной категории.
    /// </summary>
    public List<ProductDto>? Products { get; init; }
}