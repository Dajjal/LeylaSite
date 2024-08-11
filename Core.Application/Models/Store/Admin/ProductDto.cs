using Core.Domain.Abstracts;

namespace Core.Application.Models.Store.Admin;

/// <summary>
///     Представляет товар в модели передачи данных (DTO).
/// </summary>
public class ProductDto(
    string name,
    string description,
    decimal cost,
    Guid categoryId) : AbstractGuidModel
{
    /// <summary>
    ///     Наименование товара.
    /// </summary>
    public string Name { get; init; } = name;

    /// <summary>
    ///     Описание товара.
    /// </summary>
    public string Description { get; init; } = description;

    /// <summary>
    ///     Цена товара.
    /// </summary>
    public decimal Cost { get; init; } = cost;

    /// <summary>
    ///     Категория товара.
    /// </summary>
    public CategoryDto? Category { get; init; }

    /// <summary>
    ///     Идентификатор категории товара.
    /// </summary>
    public Guid CategoryId { get; init; } = categoryId;
}