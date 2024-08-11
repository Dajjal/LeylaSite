using Ardalis.Specification;
using Core.Domain.Abstracts;

namespace Core.Application.Specifications;

/// <summary>
///     Спецификация которая фильтрует только не удалённые обьекты с таблиц
/// </summary>
/// <typeparam name="TEntity">Модель описанная в Базе Данных (Таблица)</typeparam>
public sealed class NotDeletedSpecification<TEntity> : Specification<TEntity>
    where TEntity : AbstractGuidModel
{
    public NotDeletedSpecification()
    {
        Query.Where(q => q.IsDeleted == false);
    }
}