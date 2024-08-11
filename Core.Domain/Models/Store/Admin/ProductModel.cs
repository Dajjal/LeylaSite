using Core.Domain.Abstracts;

namespace Core.Domain.Models.Store.Admin;

/// <summary>
///     Товары - Model
/// </summary>
/// <remarks>
///     <para>
///         Таблица где хранятся товары
///     </para>
/// </remarks>
/// <param name="name">Наименование товара</param>
/// <param name="description">Описание товара</param>
/// <param name="cost">Цена товара</param>
public class ProductModel(
    string name,
    string description,
    decimal cost
) : AbstractGuidModel
{
    public string Name { get; init; } = name;
    public string Description { get; init; } = description;
    public decimal Cost { get; init; } = cost;
}