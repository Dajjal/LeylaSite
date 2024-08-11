using Core.Domain.Abstracts;

namespace Core.Domain.Models.Store.Site;

/// <summary>
///     Представляет категорию товаров в системе.
/// </summary>
public class CategoryModel : AbstractGuidModel
{
    public CategoryModel(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Наименование категории не может быть пустым.", nameof(name));

        Name = name;
    }

    /// <summary>
    ///     Наименование категории.
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    ///     Список товаров, принадлежащих данной категории.
    /// </summary>
    public ICollection<ProductModel> Products { get; init; } = null!;
}