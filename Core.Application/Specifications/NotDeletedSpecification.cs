using Ardalis.Specification;
using Core.Domain.Abstracts;

namespace Core.Application.Specifications;

public sealed class NotDeletedSpecification<TEntity> : Specification<TEntity> where TEntity : AbstractGuidModel
{
    public NotDeletedSpecification()
    {
        Query.Where(q => q.IsDeleted == false);
    }
}