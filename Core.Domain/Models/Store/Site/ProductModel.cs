using Core.Domain.Abstracts;

namespace Core.Domain.Models.Store.Site;

/// <summary>
///     Представляет товар в системе.
/// </summary>
/// <remarks>
///     <para>
///         Хранит информацию о товаре, включая его наименование, описание, цену и категорию.
///     </para>
/// </remarks>
public class ProductModel : AbstractGuidModel
{
    /// <summary>
    ///     Конструктор для инициализации экземпляра класса.
    /// </summary>
    /// <param name="name">Наименование товара.</param>
    /// <param name="description">Описание товара.</param>
    /// <param name="cost">Цена товара.</param>
    /// <param name="categoryId">Идентификатор категории товара.</param>
    public ProductModel(string name, string description, decimal cost, Guid categoryId)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Наименование товара не может быть пустым.", nameof(name));

        if (cost <= 0)
            throw new ArgumentException("Цена товара должна быть положительным числом.", nameof(cost));

        Name = name;
        Description = description;
        Cost = cost;
        CategoryId = categoryId;
    }

    /// <summary>
    ///     Наименование товара.
    /// </summary>
    public string Name { get; init; }

    /// <summary>
    ///     Описание товара.
    /// </summary>
    public string Description { get; init; }

    /// <summary>
    ///     Цена товара.
    /// </summary>
    public decimal Cost { get; init; }

    /// <summary>
    ///     Идентификатор категории товара.
    /// </summary>
    public Guid CategoryId { get; init; }

    /// <summary>
    ///     Категория товара.
    /// </summary>
    public CategoryModel Category { get; init; } = null!;
}