using Ardalis.Specification;
using Core.Domain.Abstracts;

namespace Core.Application.Specifications;

/// <summary>
///     Спецификация, которая фильтрует только не удалённые объекты в таблице.
///     Фильтрация осуществляется по полю <see cref="AbstractGuidModel.IsDeleted" /> с значением <c>false</c>.
/// </summary>
/// <typeparam name="TEntity">Модель, описанная в базе данных (таблица).</typeparam>
public sealed class NotDeletedSpecification<TEntity> : Specification<TEntity>
    where TEntity : AbstractGuidModel
{
    /// <summary>
    ///     Конструктор спецификации.
    ///     Инициализирует запрос фильтрацией по полю <see cref="AbstractGuidModel.IsDeleted" />,
    ///     чтобы включать только записи, где значение равно <c>false</c>.
    /// </summary>
    public NotDeletedSpecification()
    {
        // Фильтруем записи, где поле IsDeleted равно false
        Query.Where(entity => !entity.IsDeleted);
    }
}