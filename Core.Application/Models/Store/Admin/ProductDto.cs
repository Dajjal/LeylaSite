using Core.Domain.Abstracts;

namespace Core.Application.Models.Store.Admin;

/// <summary>
///     Товары - DTO
/// </summary>
/// <remarks>
///     <para>
///         Модель для передачи клиенту
///     </para>
/// </remarks>
/// <param name="name">Наименование товара</param>
/// <param name="description">Описание товара</param>
/// <param name="cost">Цена товара</param>
public class ProductDto(
    string name,
    string description,
    decimal cost
) : AbstractGuidModel
{
    public string Name { get; init; } = name;
    public string Description { get; init; } = description;
    public decimal Cost { get; init; } = cost;
}